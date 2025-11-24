using UnityEngine;

public abstract class Skill 
{
    public Skill(float skillCost)
    {
        SkillCost = skillCost;
    }

    public float SkillCost { get; protected set; }
    public abstract void UseSkill();
}
