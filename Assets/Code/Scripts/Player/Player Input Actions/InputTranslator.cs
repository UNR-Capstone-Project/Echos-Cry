using System;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Player Input Translator")]
public class InputTranslator : ScriptableObject, PlayerInputs.IGameplayActions
{
    private PlayerInputs _playerInputs;
    public event Action<Vector2> OnMovementEvent;
    public event Action OnLightAttackEvent;
    public event Action OnHeavyAttackEvent;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Gameplay.SetCallbacks(this);
    }
    private void OnEnable()
    {
        if (_playerInputs == null)
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Gameplay.SetCallbacks(this);
        }
        _playerInputs.Gameplay.Enable();
    }
    private void OnDisable()
    {
        _playerInputs.Gameplay.Disable();
    }
    private void OnDestroy()
    {
        _playerInputs.Gameplay.RemoveCallbacks(this);
        _playerInputs = null;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        OnMovementEvent?.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started) OnLightAttackEvent?.Invoke();
    }
    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started) OnHeavyAttackEvent?.Invoke();
    }
}
