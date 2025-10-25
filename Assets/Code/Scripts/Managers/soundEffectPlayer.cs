using System;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using Random = UnityEngine.Random;

public class soundEffectPlayer : MonoBehaviour
{
    public soundEffect soundData;
    [NonSerialized] private List<AudioSource> sfxAudioSource;

    private void Awake()
    {
        sfxAudioSource = new List<AudioSource>();
    }

    public void setupSoundEffect(soundEffect sound)
    {
        soundData = sound;

        if (sound.soundClips.Length > 0)
        {
            for (int i = 0; i < sound.soundClips.Length; i++)
            {
                sfxAudioSource.Add(gameObject.AddComponent<AudioSource>());
                sfxAudioSource[i].clip = sound.soundClips[i];
                sfxAudioSource[i].outputAudioMixerGroup = sound.soundMixerGroup;
                sfxAudioSource[i].loop = true;
                sfxAudioSource[i].playOnAwake = false;
            }
            
        } 
    }

    public void Play()
    {
        int random;

        if (sfxAudioSource != null)
        {
            if (sfxAudioSource.Count > 0 && soundData.randomized)
            {
                random = Random.Range(0, sfxAudioSource.Count - 1);
                sfxAudioSource[random].Play();
            }
            else
            {
                sfxAudioSource[0].Play();
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
    }
    
    public void setupName()
    {
        
    }

}
