using UnityEngine;

public class SkillManager : MonoBehaviour
{
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

    public void HandleSkill1()
    {
        Debug.Log("Skill 1");
    }
    public void HandleSkill2()
    {
        Debug.Log("Skill 2");
    }
    public void HandleSkill3()
    {
        Debug.Log("Skill 3");
    }
}
