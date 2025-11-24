using UnityEngine;

public abstract class Skill 
{
    public float SkillCost { get; protected set; }
    public abstract void UseSkill();
}
