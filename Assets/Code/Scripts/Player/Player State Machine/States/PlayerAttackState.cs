using UnityEngine;

public class PlayerAttackState : PlayerActionState
{
    public PlayerAttackState(Player playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache) 
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void Enter()
    {

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
            _playerContext.Movement.MomentumPush(_playerContext.Orientation.Direction);
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
    }
}
