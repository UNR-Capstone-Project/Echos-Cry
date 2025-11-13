using System;
using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class soundEffectPlayer : MonoBehaviour
{
    public soundEffect soundData;
    private bool hasReleased = false;
    private bool soundPlaying = false;
    [NonSerialized] private List<AudioSource> sfxAudioSource;

    private void Awake()
    {
        sfxAudioSource = new List<AudioSource>();
    }

    public void setupSoundEffect(soundEffect sound)
    {
        soundData = sound;

        foreach (var src in sfxAudioSource)
        {
            Destroy(src);
        }
        sfxAudioSource.Clear();

        if (sound.soundClips.Length > 0)
        {
            for (int i = 0; i < soundData.soundClips.Length; i++)
            {
                sfxAudioSource.Add(gameObject.AddComponent<AudioSource>());
                sfxAudioSource[i].clip = soundData.soundClips[i];
                sfxAudioSource[i].outputAudioMixerGroup = soundData.soundMixerGroup;
                sfxAudioSource[i].loop = soundData.ambience;
                sfxAudioSource[i].playOnAwake = false;
                sfxAudioSource[i].volume = soundData.soundVolume;
            }
        } 
    }

    public bool IsSoundPlaying()
    {
        return soundPlaying;
    }

    public void Play()
    {
        if (sfxAudioSource != null)
        {
            soundPlaying = true;
            if (sfxAudioSource.Count > 0 && soundData.randomized)
            {
                int random = Random.Range(0, sfxAudioSource.Count);
                sfxAudioSource[random].Play();

                if (!soundData.ambience)
                {
                    float clipLength = sfxAudioSource[random].clip.length;
                    StartCoroutine(returnToPoolAfterTime(clipLength));
                }
            }
            else
            {
                sfxAudioSource[0].Play();
                if (!soundData.ambience)
                {
                    float clipLength = sfxAudioSource[0].clip.length;
                    StartCoroutine(returnToPoolAfterTime(clipLength));
                }
            }
        }
        
    }

    public void Stop()
    {
        if (sfxAudioSource != null)
        {
            for (int i = 0; i < sfxAudioSource.Count; i++)
            {
                sfxAudioSource[i].Stop();
            }
        }
        
        ReleaseToPool();
    }

    private IEnumerator returnToPoolAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        ReleaseToPool();
    }

    private void ReleaseToPool()
    {
        if (hasReleased) { return; }
        hasReleased = true;
        soundPlaying = false;

        if (soundEffectManager.Instance != null)
        {
            soundEffectManager.Instance.releasePlayer(this);
        }
    }

    private void OnEnable()
    {
        hasReleased = false;
    }
}
