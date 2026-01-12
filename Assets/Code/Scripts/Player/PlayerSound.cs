using AudioSystem;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerSound : MonoBehaviour
{
    private void LateUpdate()
    {
        PlayFootstep();
    }

    private void PlayFootstep()
    {
        if (PlayerMovement.PlayerLocomotion != Vector2.zero
            && !_builderRef.GetSoundPlayer().IsSoundPlaying())
        {
            _builderRef
            .setSound(_footstepEffect)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        }
    }

    private void PlayHitQuality()
    {
        if (TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.EXCELLENT)
            _builderRef
            .setSound(_excellentEffect)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else if (TempoManager.CurrentHitQuality == TempoManager.HIT_QUALITY.GOOD)
            _builderRef
            .setSound(_goodEffect)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        else
            _builderRef
            .setSound(_missEffect)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    private void PlayDash()
    {
        _builderRef
        .setSound(_dashEffect)
        .setSoundPosition(transform.position)
        .ValidateAndPlaySound();
    }

    private void Start()
    {
        _builderRef = soundEffectManager.Instance.Builder;

        PlayerMovement.OnDashStarted += PlayDash;
        _inputTranslator.OnLightAttackEvent += PlayHitQuality;
        _inputTranslator.OnHeavyAttackEvent += PlayHitQuality;
    }

    private void OnDestroy()
    {
        PlayerMovement.OnDashStarted -= PlayDash;
        _inputTranslator.OnLightAttackEvent -= PlayHitQuality;
        _inputTranslator.OnHeavyAttackEvent -= PlayHitQuality;
    }

    private soundBuilder _builderRef;

    [SerializeField] soundEffect _footstepEffect;
    [SerializeField] soundEffect _dashEffect;
    [SerializeField] soundEffect _missEffect;
    [SerializeField] soundEffect _goodEffect;
    [SerializeField] soundEffect _excellentEffect;
    [SerializeField] private InputTranslator _inputTranslator;
}
