using UnityEngine;
using AudioSystem;
using Unity.VisualScripting;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private soundEffect sound;

    private void Start()
    {
        _builderRef = SoundEffectManager.Instance.Builder;
        PlaySound(sound);
    }

    private void PlaySound(soundEffect sound)
    {
        _builderRef
        .SetSound(sound)
        .SetSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }

    private SoundBuilder _builderRef;
}
