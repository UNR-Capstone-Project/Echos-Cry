using System.Collections;
using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    Coroutine currentCoroutine;

    public PlayerAttackState(Player playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void Update()
    {
        //Check if the current attack is done.
        if (_playerContext.WeaponHolder.IsActionEnded())
        {
            int hitCount = _playerContext.WeaponHolder.CurrentlyEquippedWeapon.HitColliders.Count;

            for (int i = 0; i < hitCount; i++)
            {
                TempoConductor.HitQuality hitQuality = _playerContext.WeaponHolder.CurrentlyEquippedWeapon.HitColliders[i].hit;
                _playerContext.ComboMeter.AddToComboMeter(hitQuality);
            }

            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
            _playerContext.InvokeAttackEnded();
        }

        _playerContext.Animator.UpdateMainSpriteDirection(_playerStateMachine.Locomotion);
    }

    public override void Enter()
    {
        //_playerContext.ComboMeter.ResetComboMultiplier();

        //Initialize whatever attack is happening
        if (_playerStateMachine.UsingPrimaryAction)
        {
            _playerContext.WeaponHolder.PrimaryAction();
        }
        else if (_playerStateMachine.UsingSecondaryAction)
        {
            _playerContext.WeaponHolder.SecondaryAction();
        }

        if (_playerContext.WeaponHolder.HasWeapon)
        {
            _playerContext.Animator.SpriteAnimator.Play("Attack");
        }

        _playerContext.Orientation.IsRotating = false;
        _playerContext.Animator.SpriteAnimator.Play("Run");
        _playerContext.SFX.Execute(_playerContext.SFXConfig.FootstepSFX, _playerContext.transform, 0);
        currentCoroutine = _playerContext.StartCoroutine(RepeatSoundFootstep(_playerContext.SFXConfig.FootstepSFX.soundClips[0].length));
    }
    public override void Exit()
    {
        _playerContext.Orientation.IsRotating = true;
        _playerStateMachine.IsAttacking = false;

        if (currentCoroutine != null) _playerContext.StopCoroutine(currentCoroutine);
    }

    public override void FixedUpdate()
    {
        _playerContext.Movement.Move(_playerStateMachine.Locomotion);
    }
    private IEnumerator RepeatSoundFootstep(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);
        _playerContext.SFX.Execute(_playerContext.SFXConfig.FootstepSFX, _playerContext.transform, 0);
        currentCoroutine = _playerContext.StartCoroutine(RepeatSoundFootstep(_playerContext.SFXConfig.FootstepSFX.soundClips[0].length));
    }
}
