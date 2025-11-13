using UnityEngine;

public class ComboState 
{
    public ComboState(ComboState lightAttack, ComboState heavyAttack, Attack attackData)
    {
        NextLightAttack = lightAttack;
        NextHeavyAttack = heavyAttack;
        AttackData = attackData;
    }
    public ComboState()
    {
        NextLightAttack = null;
        NextHeavyAttack = null;
        AttackData = null;
    }
    //Where to act on Attack data
    public void InitiateComboState(Animator attackAnimator)
    {
        //Debug.Log(AttackData.name);
        attackAnimator.runtimeAnimatorController = AttackData.OverrideController;
        attackAnimator.Play("Attack");
    }

    public ComboState NextLightAttack;
    public ComboState NextHeavyAttack;
    public Attack AttackData;
}
