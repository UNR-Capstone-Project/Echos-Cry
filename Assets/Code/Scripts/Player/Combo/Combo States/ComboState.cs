using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Combo System/Combo State")]
public class ComboState : ScriptableObject
{
    //Where to act on Attack data
    public void InitiateComboState()
    {
        Debug.Log(DebugText);
    }
    public enum AttackInput
    {
        UNASSIGNED = 0,
        LIGHT_ATTACK,
        HEAVY_ATTACK
    }

    public AttackInput ComboAttackInput = AttackInput.UNASSIGNED;

    public ComboState StartState = null;
    public ComboState NextLightAttack = null;
    public ComboState NextHeavyAttack = null;
    public string DebugText = "";
}

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
                else comboState.NextHeavyAttack.StartState = comboState.StartState;
            }
            if (comboState.NextLightAttack != null)
            {
                if (comboState.NextLightAttack.ComboAttackInput != ComboState.AttackInput.LIGHT_ATTACK)
                {
                    comboState.NextLightAttack = null;
                    Debug.LogError("Only ComboState with a ComboAttackInput of LIGHT_ATTACK can be assigned!");
                }
                else comboState.NextLightAttack.StartState = comboState.StartState;
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
