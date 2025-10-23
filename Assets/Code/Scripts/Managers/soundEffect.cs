using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "soundEffect", menuName = "Scriptable Objects/soundEffect")]
public class soundEffect : ScriptableObject
{
    [Range(0f, 1f), SerializeField] private float sfxVolume = .1f;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private AudioMixerGroup sfxMixerGroup;
    public bool frequentlyPlayed;
    public bool clipsAreRandomized;
    public bool isAmbience;

    public float soundVolume => sfxVolume;
    public AudioClip[] soundClips => sfxClips;
    public AudioMixerGroup soundMixerGroup => sfxMixerGroup;
}
