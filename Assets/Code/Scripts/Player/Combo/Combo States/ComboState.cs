using UnityEngine;
using UnityEditor;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Combo System/Combo State")]
public class ComboState : ScriptableObject
{
    void InitiateComboState()
    {
        //Initiates AttackFunctions
    }
    //ComboState TransitionToNextComboState()
    //{
    //    ComboState newComboState;

    //    return newComboState;
    //}
    public enum AttackInput
    {
        UNASSIGNED = 0,
        LIGHT_ATTACK,
        HEAVY_ATTACK
    }
    public enum StateType
    {
        UNASSIGNED = 0,
        START_STATE,
        COMBO_STATE
    }

    public AttackInput ComboAttackInput;
    public StateType ComboStateType;

    [SerializeField] public ComboState StartState;
    [SerializeField] public ComboState NextLightAttack;
    [SerializeField] public ComboState NextHeavyAttack;
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
            }
            if (comboState.NextLightAttack != null)
            {
                if (comboState.NextLightAttack.ComboAttackInput != ComboState.AttackInput.LIGHT_ATTACK)
                {
                    comboState.NextLightAttack = null;
                    Debug.LogError("Only ComboState with a ComboAttackInput of LIGHT_ATTACK can be assigned!");
                }
            }

            if (comboState.StartState != null)
            {
                if(comboState.StartState.ComboStateType != ComboState.StateType.START_STATE)
                {
                    comboState.StartState = null;
                    Debug.LogError("Only a ComboState registered as a START_STATE in ComboStateType can be assigned!");
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
            if (comboState.StartState == comboState)
            {
                comboState.StartState = null;
            }
        }
    }
}
