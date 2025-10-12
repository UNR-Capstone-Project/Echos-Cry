using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Combo System/Combo State")]
public class ComboState : ScriptableObject
{
    //Where to act on Attack data
    public void InitiateComboState(Animator comboStateMachineAnimator)
    {
        Debug.Log("Combo State:" + this.name);
        comboStateMachineAnimator.runtimeAnimatorController = ComboAttack.OverrideAnimation;
        comboStateMachineAnimator.Play(Animator.StringToHash("Attack"));
    }

    public enum AttackInput
    {
        UNASSIGNED = 0,
        LIGHT_ATTACK,
        HEAVY_ATTACK
    }

    public AttackInput ComboAttackInput = AttackInput.UNASSIGNED;

    public ComboState NextLightAttack = null;
    public ComboState NextHeavyAttack = null;
    public Attack ComboAttack = null;
}

#if UNITY_EDITOR
[CustomEditor(typeof(ComboState))]
public class ComboStateCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ComboState comboState = (ComboState)target;
        if (GUI.changed) {
            if (comboState.NextHeavyAttack != null)
            {
                if (comboState.NextHeavyAttack.ComboAttackInput != ComboState.AttackInput.HEAVY_ATTACK)
                {
                    comboState.NextHeavyAttack = null;
                    Debug.LogError("Only ComboState with a ComboAttackInput of HEAVY_ATTACK can be assigned!");
                }

            }
            if (comboState.NextLightAttack != null)
            {
                if (comboState.NextLightAttack.ComboAttackInput != ComboState.AttackInput.LIGHT_ATTACK)
                {
                    comboState.NextLightAttack = null;
                    Debug.LogError("Only ComboState with a ComboAttackInput of LIGHT_ATTACK can be assigned!");
                }

            }

            if(comboState.NextHeavyAttack == comboState)
            {
                comboState.NextHeavyAttack = null;
            }
            if (comboState.NextLightAttack == comboState)
            {
                comboState.NextLightAttack = null;
            }
        }
    }
}
#endif