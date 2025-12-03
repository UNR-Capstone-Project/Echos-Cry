using UnityEngine;
using UnityEngine.Audio;

/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By: 

namespace AudioSystem {
    /// <summary>
    /// a ScriptableObject to hold all the relevant data for a sound effect in the game
    /// </summary>
    [CreateAssetMenu(fileName = "soundEffect", menuName = "Scriptable Objects/soundEffect")]
    public class soundEffect : ScriptableObject
    {
        [Range(0f, 1f), SerializeField] private float sfxVolume = .1f;
        [SerializeField] private AudioClip[] sfxClips;
        [SerializeField] private AudioMixerGroup sfxMixerGroup;
        [SerializeField] private bool frequentlyPlayed;
        [SerializeField] private bool clipsAreRandomized;
        [SerializeField] private bool isAmbience;

        public float soundVolume => sfxVolume;
        public AudioClip[] soundClips => sfxClips;
        public AudioMixerGroup soundMixerGroup => sfxMixerGroup;
        public bool isFrequent => frequentlyPlayed;
        public bool randomized => clipsAreRandomized;
        public bool ambience => isAmbience;
    }
}