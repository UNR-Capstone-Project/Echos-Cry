using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Player Input Translator")]
public class InputTranslator : ScriptableObject, PlayerInputs.IGameplayActions, PlayerInputs.IPauseMenuActions, PlayerInputs.IPlayerMenuActions
{
    private PlayerInputs _playerInputs;
    public event Action<Vector2> OnMovementEvent;
    public event Action OnDashEvent;
    public event Action OnLightAttackEvent;
    public event Action OnHeavyAttackEvent;
    public static event Action OnPauseEvent, OnPauseDownInput, OnPauseUpInput;
    public static event Action OnResumeEvent;
    public static event Action OnMapEvent;
    public static event Action OnExitMapEvent, OnJournalLeftInput, OnJournalRightInput;

    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Gameplay.SetCallbacks(this);
        _playerInputs.PauseMenu.SetCallbacks(this);
        _playerInputs.PlayerMenu.SetCallbacks(this);
    }
    private void OnEnable()
    {
        if (_playerInputs == null)
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Gameplay.SetCallbacks(this);
            _playerInputs.PauseMenu.SetCallbacks(this);
            _playerInputs.PlayerMenu.SetCallbacks(this);
        }
        _playerInputs.Gameplay.Enable();
        _playerInputs.PauseMenu.Disable();
        _playerInputs.PlayerMenu.Disable();
    }
    private void OnDisable()
    {
        _playerInputs.Gameplay.Disable();
        _playerInputs.PauseMenu.Disable();
        _playerInputs.PlayerMenu.Disable();
    }
    private void OnDestroy()
    {
        _playerInputs.Gameplay.RemoveCallbacks(this);
        _playerInputs.PlayerMenu.RemoveCallbacks(this);
        _playerInputs.PauseMenu.RemoveCallbacks(this);
        _playerInputs = null;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        OnMovementEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started) OnDashEvent?.Invoke();
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started) OnLightAttackEvent?.Invoke();
    }
    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started) OnHeavyAttackEvent?.Invoke();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnPauseEvent?.Invoke();
            _playerInputs.PauseMenu.Enable();
            _playerInputs.Gameplay.Disable();
        }
    }

    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnMapEvent?.Invoke();
            _playerInputs.Gameplay.Disable();
            _playerInputs.PlayerMenu.Enable();
        }
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnResumeEvent?.Invoke();
            _playerInputs.Gameplay.Enable();
            _playerInputs.PauseMenu.Disable();
        }
    }

    public void OnExitMenuMap(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnExitMapEvent?.Invoke();
            _playerInputs.Gameplay.Enable();
            _playerInputs.PlayerMenu.Disable();
        }
    }

    public void OnNavUp(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnPauseUpInput?.Invoke();
        }
    }
    
    public void OnNavDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnPauseDownInput?.Invoke();
        }
    }

    public void OnNavRight(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnJournalRightInput?.Invoke();
        }
    }

    public void OnNavLeft(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnJournalLeftInput?.Invoke();
        }
    }
}
