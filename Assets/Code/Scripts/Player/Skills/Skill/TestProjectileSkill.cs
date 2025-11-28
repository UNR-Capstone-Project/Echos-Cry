using UnityEngine;

public class TestProjectileSkill : Skill
{
    GameObject m_Projectile;
    float m_speed;
    public TestProjectileSkill(float skillCost, GameObject projectile, float speed) : base(skillCost)
    {
        m_Projectile = projectile;
        m_speed = speed;
    }

    public override void UseSkill()
    {
        GameObject.Instantiate(m_Projectile, PlayerRef.PlayerTransform.position, m_Projectile.transform.rotation).GetComponent<Rigidbody>().AddForce(PlayerDirection.AimDirection * m_speed, ForceMode.Impulse);
    }
}
