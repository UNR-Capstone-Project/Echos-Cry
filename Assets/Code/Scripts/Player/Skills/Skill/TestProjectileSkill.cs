using UnityEngine;

public class TestProjectileSkill : Skill
{
    RBProjectilePool handler;
    float m_speed;
    public TestProjectileSkill(float skillCost, RBProjectilePool projectileHandler) : base(skillCost)
    {
        handler = projectileHandler;
    }

    public override void UseSkill()
    {
        //handler.UseProjectile(PlayerRef.Transform.position, PlayerOrientation.Direction, 5f);
    }
}
