using UnityEngine;

public class PlayerDeathState : PlayerActionState
{
    public PlayerDeathState(Player playerContext, PlayerStateMachine playerStateMachine, PlayerStateCache playerStateCache)
        : base(playerContext, playerStateMachine, playerStateCache) { }

    public override void Enter()
    {
        Debug.Log("Death");
        GameManager.HandlePlayerDeath(_playerContext);
        _playerContext.Reset();
    }
}
