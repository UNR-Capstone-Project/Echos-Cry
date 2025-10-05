using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SoundSystem;
public class MusicPlayer : MonoBehaviour
{
    private double startTime = AudioSettings.dspTime;
    private float playerVolume = 1.0f;
    private MusicEvent song;
    private List<AudioSource> songLayers = new List<AudioSource>();
    private Coroutine volumeFadingRoutine = null;

    public List<float> startingSongLayerVolumes = new List<float>();

    public void SetupSong(MusicEvent givenSong)
    {
        //Debug.Log("setupSong called with: " + (givenSong == null ? "NULL" : givenSong.name));
        song = givenSong;
        SetupLayers();
    }

    public void SetupLayers()
    {
        songLayers.Clear();

        for (int i = 0; i < MusicManager.MAX_LAYER_COUNT; i++)
        {
            songLayers.Add(gameObject.AddComponent<AudioSource>());
            Debug.Log("added Audio Source to a music player");
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
        if (musicLayerIndex < 0 || musicLayerIndex > song.Layers.Length)
        {
            Debug.Log("Music Layer selected for changing volume exceeds layers available or is a negative value");
            return;
        }

        AudioSource layer = songLayers[musicLayerIndex];
        layer.volume = newVolume;
    }

    public void Play()
    {
        //Debug.Log("play() called with song: " + (song == null ? "NULL" : song.name));

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
    }

    public void Stop()
    {
        if (song == null) return;

        foreach (var layer in songLayers)
        {
            if (layer == null) continue;

            layer.Stop();
        }
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
