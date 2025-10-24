using System.Collections.Generic;
using AudioSystem;
using UnityEngine;

public class soundEffectPlayer : MonoBehaviour
{
    [SerializeField] public soundEffect soundData;
    private List<AudioSource> sfxAudioSource = new List<AudioSource>();

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
        if (sfxAudioSource.Count > 0 && soundData.clipsAreRandomized)
        {
            random = Random.Range(0, sfxAudioSource.Count - 1);
            sfxAudioSource[random].Play();
        }
        else
        {
            sfxAudioSource[0].Play();
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

}
