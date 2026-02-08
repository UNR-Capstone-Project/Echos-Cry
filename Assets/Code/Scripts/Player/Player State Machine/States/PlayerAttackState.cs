using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(Player playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void Enter()
    {
        _playerContext.ComboMeter.ResetComboMultiplier();

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
    }

    public override void Exit()
    {
        _playerContext.Orientation.IsRotating = true;
        _playerStateMachine.IsAttacking = false;
    }

    public override void Update()
    {
        //check if current attack is done
        if (_playerContext.WeaponHolder.IsActionEnded())
        {
            int hitCount = _playerContext.WeaponHolder.CurrentlyEquippedWeapon.HitColliders.Count;
            if (hitCount > 0) _playerContext.ComboMeter.AddToComboMeter(hitCount);
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
            _playerContext.InvokeAttackEnded();
        }
    }
}
