using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SoundSystem;
using static Unity.Burst.Intrinsics.X86.Avx;

public class MusicPlayer : MonoBehaviour
{
    private MusicEvent song;
    private List<AudioSource> songLayers = new List<AudioSource>();
    private Coroutine volumeFadingRoutine = null;

    public List<float> startingSongLayerVolumes = new List<float>();

    //Metronome Variables
    private bool songRunning = false;
    private volatile float sampleProgress;
    private volatile float sampleTime;
    public float SampleProgress => sampleProgress;
    public float SampleTime => sampleTime;

    private double startTime;
    private double nextTime;

    public float bpm = 100f;
    public float gain = 0.5f; //Volume of tick!
    public int signatureHi = 4;
    public int signatureLo = 4;
    private float amp = 0f;
    private float phase = 0f;
    private double sampleRate = 0f;
    private int accent;
    private bool tickEnabled = true;

    //This function was provided by Unity's documentation - https://docs.unity3d.com/6000.2/Documentation/ScriptReference/AudioSettings-dspTime.html
    //Addressables - Control when assets are loaded in
    //Thread sleeping to create a "lag" effect to test
    //Know when audio is actually played from Audio source for synchronization.
    
    void OnAudioFilterRead(float[] data, int channels) //This callback is executed on the audio thread when an audio buffer is read from an AudioSource
    {
        //ISSUE: The tick sound is being written into the audio buffer that the music plays from, therefore it is added onto the music's sound wave. This means volume control of the tick sound is difficult to lower without lowering overall music volume.
        if (!tickEnabled) { return; }
        if (!songRunning) { return; }

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;
        int dataLen = data.Length / channels;
        int n = 0;

        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;

            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }

            while (sample + n >= nextTime)
            {
                nextTime += samplesPerTick;
                amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    amp *= 2.0F;
                }
                //Debug.Log("Tick: " + accent + "/" + signatureHi);
            }

            sampleProgress = Mathf.Clamp01((float)((nextTime - sample) / samplesPerTick));
            sampleTime = (float)((samplesPerTick / sampleRate) - ((nextTime - (sample + n)) / sampleRate));

            phase += amp * 0.3F;
            amp *= 0.993F;
            n++;
        }
    }

    public void DisableTick()
    {
        tickEnabled = false;
    }

    public void SetupSong(MusicEvent givenSong)
    {
        //Debug.Log("setupSong called with: " + (givenSong == null ? "NULL" : givenSong.name));
        song = givenSong;
        SetupLayers();
    }

    public void SetupLayers()
    {
        songLayers.Clear();

        //Setup initial gameobject with AudioSource
        songLayers.Add(gameObject.GetComponent<AudioSource>());
        songLayers[0].playOnAwake = false;
        songLayers[0].loop = true;

        //Create copy of main gameObject for subsequent objects to copy
        GameObject childObject = Instantiate(gameObject, transform.position, Quaternion.identity, transform);
        Destroy(childObject.GetComponent<MusicPlayer>());
        songLayers.Add(childObject.GetComponent<AudioSource>());
        songLayers[1].playOnAwake = false;
        songLayers[1].loop = true;

        for (int i = 2; i < MusicManager.MAX_LAYER_COUNT; i++)
        {
            //Copy base gameObject into a child gameObject that will have individual AudioSources
            GameObject newObject = Instantiate(childObject, transform.position, Quaternion.identity, transform);
            songLayers.Add(newObject.GetComponent<AudioSource>());
            songLayers[i].playOnAwake = false;
            songLayers[i].loop = true;
        }
    }

    public void SetVolume(float newVolume)
    {
        //validation
        newVolume = Mathf.Clamp(newVolume, 0, 1);
        foreach (var layer in songLayers)
        {
            if (layer == null) continue;

            layer.volume = newVolume;
        }
    }

    public void SetVolume(int musicLayerIndex, float newVolume)
    {
        //validation
        newVolume = Mathf.Clamp(newVolume, 0, 1);
        if (musicLayerIndex < 0 || musicLayerIndex >= song.Layers.Length)
        {
            Debug.Log("Music Layer selected for changing volume exceeds layers available or is a negative value");
            return;
        }

        AudioSource layer = songLayers[musicLayerIndex];
        layer.volume = newVolume;
    }

    public void Play()
    {
        songRunning = true;

        startTime = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTime = startTime * sampleRate;

        if (song == null)
        {
            Debug.Log("Song in the music player has a null musicEvent");
            return;
        }

        //iterate through existing layers with audio 
        for (int i = 0; i < song.Layers.Length; i++)
        {
            songLayers[i].clip = song.Layers[i];
            songLayers[i].volume = song.Volume;
            songLayers[i].loop = true;
            songLayers[i].PlayScheduled(startTime);
        }
    }

    public void Stop()
    {
        if (song == null) return;

        foreach (var layer in songLayers)
        {
            if (layer == null) continue;

            layer.Stop();
        }

        songRunning = false;
    }

    public void Pause()
    {
        if (song == null) return;
        
        foreach (var layer in songLayers)
        {
            if (layer.isPlaying)
            {
                layer.Pause();
            }
        }

        songRunning = false;
    }

    public void Resume()
    {
        
        if (song == null) return;

        foreach (var layer in songLayers)
        {
            if (!layer.isPlaying)
            {
                layer.UnPause();
            }
        }

        songRunning = true;
    }
    
    /// <summary>
    /// fades the volume of a player to a volume of 0 over a period of given seconds (float fadeTime)
    /// </summary>
    /// <param name="fadeTime">the amount of the time, in seconds, to reach a destination volume of zero</param>
    public void FadeVolume(float fadeTime)
    {
        //validation 
        if (fadeTime < 0)
        {
            fadeTime = 0.5f;
        }

        if (volumeFadingRoutine != null)
        {
            StopCoroutine(volumeFadingRoutine);
        }

        volumeFadingRoutine = StartCoroutine(FadeVolumeCoroutine(0, fadeTime));

    }

    /// <summary>
    /// changes the volume of a player to the destinationVolume (float) over a period of seconds (float fadeTime)
    /// </summary>
    /// <param name="destinationVolume">the desired end volume for a musicPLayer of a song to hit</param>
    /// <param name="fadeTime">the amount of the time, in seconds, to reach the destination volume</param>
    public void FadeVolume(float destinationVolume, float fadeTime)
    {
        //validation 
        if (fadeTime < 0)
        {
            fadeTime = 0.5f;
        }

        //kill coroutine if one is currently going, prevent players from animating same song at same time 
        if (volumeFadingRoutine != null)
        {
            StopCoroutine(volumeFadingRoutine);
        }

        volumeFadingRoutine = StartCoroutine(FadeVolumeCoroutine(destinationVolume, fadeTime));
    }

    public IEnumerator FadeVolumeCoroutine(float targetVolume, float fadeTime)
    {
        float startVolume;
        float newVolume;

        //clear current volume list and attach new volumes on start of coroutine
        startingSongLayerVolumes.Clear();

        for (int i = 0; i < songLayers.Count; i++)
        {
            startingSongLayerVolumes.Add(songLayers[i].volume);
        }

        for (float elapsedTime = 0; elapsedTime < fadeTime; elapsedTime += Time.deltaTime)
        {
            //each frame, iterate through each layer and modulate its volume
            for (int i = 0; i < songLayers.Count; i++)
            {
                startVolume = startingSongLayerVolumes[i];
                newVolume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeTime);
                songLayers[i].volume = newVolume;
            }

            yield return null;
        }

        //ensuring accuracy of volume, boundary check for fadeTime
        for (int i = 0; i < songLayers.Count; i++)
        {
            songLayers[i].volume = targetVolume;
        }
    }
}
