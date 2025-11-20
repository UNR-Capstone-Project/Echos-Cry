using AudioSystem;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private void LateUpdate()
    {
        PlayFootstep();
    }

    private void PlayFootstep()
    {
        if (PlayerMovement.PlayerRigidbody.linearVelocity != Vector3.zero
            && !_builderRef.GetSoundPlayer().IsSoundPlaying())
        {
            _builderRef
            .setSound(_footstepEffect)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        }
    }

    private void Start()
    {
        _builderRef = soundEffectManager.Instance.Builder;
    }

    private soundBuilder _builderRef;
    [SerializeField] soundEffect _footstepEffect;
}
