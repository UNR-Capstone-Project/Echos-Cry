using System;
using System.Collections;
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
                sfxAudioSource[i].loop = sound.ambience;
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
    }

    private IEnumerator returnToPoolAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        soundEffectManager.Instance.releasePlayer(this);
    }

}
