using UnityEngine;

public class TestProjectileSkill : Skill
{
    BaseProjectileHandler handler;
    float m_speed;
    public TestProjectileSkill(float skillCost, BaseProjectileHandler projectileHandler) : base(skillCost)
    {
        handler = projectileHandler;
    }

    public override void UseSkill()
    {
        handler.UseProjectile(PlayerRef.PlayerTransform.position, PlayerDirection.AimDirection);
    }
}
