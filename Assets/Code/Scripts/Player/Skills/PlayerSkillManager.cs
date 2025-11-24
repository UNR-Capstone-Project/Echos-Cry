
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static void SetSkills(Skill newSkill, SKILL_NUM skillNum)
    {
        playerSkills[(int)skillNum] = newSkill;
    }

    public void HandleSkill1()
    {
        Debug.Log("Using Skill 1");
        Skill currentSkill = playerSkills[(int)SKILL_NUM.SKILL1];
        if (currentSkill != null)
        {
            currentSkill.UseSkill();
            PlayerComboMeter.SubtractFromComboMeter(currentSkill.SkillCost);
        }
    }
    public void HandleSkill2()
    {
        Debug.Log("Using Skill 2");
        Skill currentSkill = playerSkills[(int)SKILL_NUM.SKILL2];
        if (currentSkill != null)
        {
            currentSkill.UseSkill();
            PlayerComboMeter.SubtractFromComboMeter(currentSkill.SkillCost);
        }
    }
    public void HandleSkill3()
    {
        Debug.Log("Using Ultimate");
        Skill currentSkill = playerSkills[(int)SKILL_NUM.ULTIMATE];
        if (currentSkill != null)
        {
            currentSkill.UseSkill();
            PlayerComboMeter.SubtractFromComboMeter(currentSkill.SkillCost);
        }
    }

    private void Awake()
    {
        playerSkills = new Skill[SKILL_AMOUNT];    
    }
    private void Start()
    {
        InputTranslator.OnSkill1Event += HandleSkill1;
        InputTranslator.OnSkill2Event += HandleSkill2;
        InputTranslator.OnSkill3Event += HandleSkill3;  
    }
    private void OnDestroy()
    {
        InputTranslator.OnSkill1Event -= HandleSkill1;
        InputTranslator.OnSkill2Event -= HandleSkill2;
        InputTranslator.OnSkill3Event -= HandleSkill3;
    }

    public enum SKILL_NUM{
        SKILL1 = 0,
        SKILL2 = 1,
        ULTIMATE = 2
    }

    private const int SKILL_AMOUNT = 3;
    [SerializeField] private static Skill[] playerSkills;
}
