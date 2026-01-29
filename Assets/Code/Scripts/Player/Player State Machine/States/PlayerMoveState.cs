using System.Collections;
using UnityEngine;

public class PlayerMoveState : PlayerActionState
{
    Coroutine currentCoroutine;
    public PlayerMoveState(Player playerContext,
        PlayerStateMachine playerStateMachine, 
        PlayerStateCache playerStateCache)
        : base(playerContext, playerStateMachine, playerStateCache)
    {}

    protected override void OnCheckSwitch()
    {
        if (!_playerStateMachine.IsMoving)
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
        }
        else if (_playerStateMachine.IsAttacking && TempoConductor.Instance.IsOnBeat())
        {
            _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Attack));
        }
        else if (_playerStateMachine.CanDash && _playerStateMachine.IsDashing)
        {
            if(_playerContext.Movement.PlayerMovementConfig.IsDashToBeat && TempoConductor.Instance.IsOnBeat())
                _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Dash));
            else _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Dash));
        }
    }

    public override void Enter()
    {
        _playerContext.Animator.SpriteAnimator.Play("Run");
        _playerContext.SFX.Execute(_playerContext.SFXConfig.FootstepSFX, _playerContext.transform, 0);
        currentCoroutine = _playerContext.StartCoroutine(RepeatSoundFootstep(_playerContext.SFXConfig.FootstepSFX.soundClips[0].length));
    }
    public override void Exit()
    {
        if(currentCoroutine != null) _playerContext.StopCoroutine(currentCoroutine);
    }
    public override void Update()
    {
        _playerContext.Animator.UpdateMainSpriteDirection(_playerStateMachine.Locomotion);
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
