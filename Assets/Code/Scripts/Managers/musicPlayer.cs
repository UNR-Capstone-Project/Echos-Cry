using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SoundSystem;
using static Unity.Burst.Intrinsics.X86.Avx;

public class MusicPlayer : MonoBehaviour
{
    private float playerVolume = 1.0f;
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

    public float bpm = 84f;
    public float gain = 0.5f;
    public int signatureHi = 4;
    public int signatureLo = 4;
    private float amp = 0f;
    private float phase = 0f;
    private double sampleRate = 0f;
    private int accent;

    //This function was provided by Unity's documentation - https://docs.unity3d.com/6000.2/Documentation/ScriptReference/AudioSettings-dspTime.html
    void OnAudioFilterRead(float[] data, int channels) //This callback is executed on the audio thread when an audio buffer is read from an AudioSource
    {
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
        songLayers.Add(gameObject.AddComponent<AudioSource>());
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
        playerVolume = newVolume;
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
            Debug.Log(i);
            songLayers[i].clip = song.Layers[i];
            songLayers[i].volume = song.Volume;
            songLayers[i].loop = true;
            songLayers[i].PlayScheduled(startTime);
        }

        songRunning = true;
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
