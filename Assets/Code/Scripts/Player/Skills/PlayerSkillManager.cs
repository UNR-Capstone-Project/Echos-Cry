
using UnityEngine;

public class PlayerSkillManager : MonoBehaviour
{
    public static void SetSkills(Skill newSkill, SKILL_NUM skillNum)
    {
        playerSkills[(int)skillNum] = newSkill;
    }

    public void HandleSkill1()
    {
        Skill currentSkill = playerSkills[(int)SKILL_NUM.SKILL1];
        if (currentSkill != null /*&& PlayerComboMeter.ComboMeterAmount >= currentSkill.SkillCost*/)
        {
            Debug.Log("Using Skill 1");
            currentSkill.UseSkill();
            PlayerComboMeter.SubtractFromComboMeter(currentSkill.SkillCost);
        }
    }
    public void HandleSkill2()
    {
        Skill currentSkill = playerSkills[(int)SKILL_NUM.SKILL2];
        if (currentSkill != null /*&& PlayerComboMeter.ComboMeterAmount >= currentSkill.SkillCost*/)
        {
            Debug.Log("Using Skill 2");
            currentSkill.UseSkill();
            PlayerComboMeter.SubtractFromComboMeter(currentSkill.SkillCost);
        }
    }
    public void HandleSkill3()
    {
        Skill currentSkill = playerSkills[(int)SKILL_NUM.ULTIMATE];
        if (currentSkill != null && PlayerComboMeter.ComboMeterAmount >= currentSkill.SkillCost)
        {
            Debug.Log("Using Ultimate");
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
        //_translator.OnSkill1Event += HandleSkill1;
        //_translator.OnSkill2Event += HandleSkill2;
        //_translator.OnSkill3Event += HandleSkill3;  

        //SetSkills(new TestProjectileSkill(40f, RBProjectileManager.RequestHandler(tempPrefab)), SKILL_NUM.SKILL1);
        //SetSkills(new TestProjectileSkill(40f, RBProjectileManager.RequestHandler(tempPrefab2)), SKILL_NUM.SKILL2);
    }
    private void OnDestroy()
    {
        //_translator.OnSkill1Event -= HandleSkill1;
        //_translator.OnSkill2Event -= HandleSkill2;
        //_translator.OnSkill3Event -= HandleSkill3;
    }

    public enum SKILL_NUM{
        SKILL1 = 0,
        SKILL2 = 1,
        ULTIMATE = 2
    }

    [SerializeField] private InputTranslator _translator;
    private const int SKILL_AMOUNT = 3;
    [SerializeField] private static Skill[] playerSkills;
}
