using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    [CreateAssetMenu(fileName = "MusicEvent", menuName = "Scriptable Objects/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        [Range(0f, 1f), SerializeField] private float musicVolume = .1f;
        [SerializeField] private AudioClip[] musicLayers;
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        public int musicTempo;

        //Public Getters
        public float Volume => musicVolume;
        public AudioClip[] Layers => musicLayers;
        public AudioMixerGroup MixerGroup => musicMixerGroup;
    }
}


