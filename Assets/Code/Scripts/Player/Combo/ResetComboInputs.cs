using UnityEngine;

public class ResetComboInputs : MonoBehaviour
{
    private ComboStateMachine _comboStateMachine;
    private void Awake()
    {
        _comboStateMachine = GetComponentInParent<ComboStateMachine>();
    }
    public void ResetComboInput()
    {
        _comboStateMachine.ResetInput();
    }
}
