using UnityEngine;

public class TestProjectileSkill : Skill
{
    RBProjectileHandler handler;
    float m_speed;
    public TestProjectileSkill(float skillCost, RBProjectileHandler projectileHandler) : base(skillCost)
    {
        handler = projectileHandler;
    }

    public override void UseSkill()
    {
        handler.UseProjectile(PlayerRef.PlayerTransform.position, PlayerDirection.AimDirection, 5f);
    }
}
