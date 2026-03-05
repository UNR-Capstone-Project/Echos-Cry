using System;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;

public class RemapOptionHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _ActionNameText;
    [SerializeField] private TextMeshProUGUI _ActionBindingText;

    private InputAction _inputAction;
    private int _bindingIndex;
    private InputActionRebindingExtensions.RebindingOperation rebindingOperationKeyboard;

    public void SetupAction(string name, string binding, InputAction action, int bindingIndex)
    {
        _ActionNameText.text = name;
        _ActionBindingText.text = binding;
        _inputAction = action;
        _bindingIndex = bindingIndex;
    }

    public void OnRemapButtonPressed()
    {
        _ActionBindingText.text = "Awaiting New Input...";

        _inputAction.Disable();
        rebindingOperationKeyboard = _inputAction.PerformInteractiveRebinding()
            .WithTargetBinding(_bindingIndex)
            .OnMatchWaitForAnother(0.1f);
        rebindingOperationKeyboard.Start();
        rebindingOperationKeyboard.OnComplete(OnRebindingOperationComplete);
    }

    private void OnRebindingOperationComplete(InputActionRebindingExtensions.RebindingOperation operation)
    {
        _ActionBindingText.text = operation.action.bindings[_bindingIndex].ToDisplayString();

        operation.action.Enable();
        rebindingOperationKeyboard.Cancel();
    }
}
