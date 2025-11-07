using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    [CreateAssetMenu(fileName = "MusicEvent", menuName = "Scriptable Objects/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        [Range(0f, 1f), SerializeField] private float musicVolume = .1f;
        [Range(80f, 140f), SerializeField] private float bpm = 85f;
        [SerializeField] private AudioClip[] musicLayers;
        [SerializeField] private AudioMixerGroup musicMixerGroup;
        [SerializeField] private bool trackBPM;

        //Public Getters
        public float Volume => musicVolume;
        public float BPM => bpm;
        public bool TrackBPM => trackBPM;
        public AudioClip[] Layers => musicLayers;
        public AudioMixerGroup MixerGroup => musicMixerGroup;
    }
}


