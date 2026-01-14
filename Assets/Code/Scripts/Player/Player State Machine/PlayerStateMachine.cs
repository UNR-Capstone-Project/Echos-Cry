using UnityEngine;

public class PlayerStateMachine : AbstractStateMachine<PlayerActionState>
{
    public bool isMoving;
    public bool isAttacking;
    public bool isLightAttacking, isHeavyAttacking;
    public bool isDashing;

    public Vector2 locomotion;
}
