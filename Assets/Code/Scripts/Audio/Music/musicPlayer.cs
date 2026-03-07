using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SoundSystem;
/// Original Author: Victor
/// All Contributors Since Creation: Victor, Michael, Andy

public class MusicPlayer : MonoBehaviour
{
    private MusicEvent song;
    private List<AudioSource> songLayers = new List<AudioSource>();
    private Coroutine volumeFadingRoutine = null;

    public List<float> startingSongLayerVolumes = new List<float>();

    //Metronome Variables
    private bool songRunning = false;
    private bool trackBeatTiming = true;

    private double startTime;
    private double nextBeatTime;

    public float bpm = 100f;
    public int signatureHi = 4;
    public int signatureLo = 4;
    private double sampleRate = 0f;
    private double totalSamplesInClip = 0;

    private volatile float sampleProgress;
    public float SampleProgress => sampleProgress;

    public void DisableBeatTracking()
    {
        trackBeatTiming = false;
    }
    public bool IsBeatTracked()
    {
        return trackBeatTiming;
    }

    void OnAudioFilterRead(float[] data, int channels) //This callback is executed on the audio thread when an audio buffer is read from an AudioSource
    {
        if (!trackBeatTiming || !songRunning) return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double currentTotalSample = (AudioSettings.dspTime * sampleRate) - (startTime * sampleRate);
        double sampleInLoop = currentTotalSample % totalSamplesInClip;
        double currentBeatNumber = System.Math.Floor(sampleInLoop / samplesPerTick);
        nextBeatTime = (currentBeatNumber + 1) * samplesPerTick;
        sampleProgress = (float)((sampleInLoop % samplesPerTick) / samplesPerTick);
    }
    public void Play()
    {
        if (song == null) return;

        Stop();

        startTime = AudioSettings.dspTime + 0.1;
        sampleRate = AudioSettings.outputSampleRate;
        nextBeatTime = startTime * sampleRate;
        totalSamplesInClip = song.Layers[0].samples;

        songRunning = true;

        //Iterate through existing layers with audio.
        for (int i = 0; i < song.Layers.Length; i++)
        {
            songLayers[i].clip = song.Layers[i];
            songLayers[i].volume = song.Volume;
            songLayers[i].loop = true;
            songLayers[i].PlayScheduled(startTime);
        }
    }

    public void SetupSong(MusicEvent givenSong)
    {
        song = givenSong;
        SetupLayers();
    }

    public void SetupLayers()
    {
        foreach (var layer in songLayers)
        {
            if (layer != null && layer.gameObject != this.gameObject)
            {
                Destroy(layer.gameObject);
            }
        }
        songLayers.Clear();

        //Setup main gameobject with AudioSource
        AudioSource mainSource = GetComponent<AudioSource>();
        if (mainSource == null) mainSource = gameObject.AddComponent<AudioSource>();

        ConfigureAudioSource(mainSource);
        songLayers.Add(mainSource);

        //Additional Layers
        for (int i = 1; i < MusicManager.MAX_LAYER_COUNT; i++)
        {
            GameObject childAudioObject = new GameObject($"MusicLayer_{i}");
            childAudioObject.transform.SetParent(this.transform);
            childAudioObject.transform.localPosition = Vector3.zero;

            AudioSource audioSource = childAudioObject.AddComponent<AudioSource>();
            ConfigureAudioSource(audioSource);
            songLayers.Add(audioSource);
        }
    }

    private void ConfigureAudioSource(AudioSource source)
    {
        source.playOnAwake = false;
        source.loop = true;
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

    public bool SelfIsPlaying()
    {
        foreach (var layer in songLayers)
        {   
            if (layer.isPlaying)
            {
                return true;
            }
        }
        return false;
    }
}
