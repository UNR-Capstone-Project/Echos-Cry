using UnityEngine;

public class PlayerStateMachine : AbstractStateMachine<PlayerActionState>
{
    public PlayerStateMachine()
    {
        isMoving = false;
        isAttacking = false;
    }

    public bool isMoving;
    public bool isAttacking;
    public bool isDashing;

    public Vector2 locomotion;
}
