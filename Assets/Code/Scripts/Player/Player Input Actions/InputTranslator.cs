using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputTranslator : MonoBehaviour, PlayerInputs.IGameplayActions, PlayerInputs.IPauseMenuActions, PlayerInputs.IPlayerMenuActions, PlayerInputs.IShopMenuActions
{
    private PlayerInputs _playerInputs;

    private static InputTranslator _instance;

    public static event Action<Vector2> OnMovementEvent;
    public static event Action          OnDashEvent;
    public static event Action          OnLightAttackEvent;
    public static event Action          OnHeavyAttackEvent;
    public static event Action          OnPauseEvent, OnPauseDownInput, OnPauseUpInput;
    public static event Action          OnResumeEvent;
    public static event Action          OnMapEvent;
    public static event Action          OnExitMapEvent, OnJournalLeftInput, OnJournalRightInput;
    public static event Action          OnSkill1Event, OnSkill2Event, OnSkill3Event;
    public static event Action          OnShopEvent;
    public static event Action          OnCloseShopEvent;
    public static event Action          OnItem1Event, OnItem2Event, OnItem3Event, OnItem4Event;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        _playerInputs = new PlayerInputs();
        _playerInputs.Gameplay.SetCallbacks(this);
        _playerInputs.PauseMenu.SetCallbacks(this);
        _playerInputs.PlayerMenu.SetCallbacks(this);
        _playerInputs.ShopMenu.SetCallbacks(this);
    }
    private void Start()
    {
        if (_instance == null) _instance = this;

        _playerInputs.Gameplay.Enable();
        _playerInputs.PauseMenu.Disable();
        _playerInputs.PlayerMenu.Disable();
        _playerInputs.ShopMenu.Disable();
    }
    private void OnEnable()
    {
        if (_playerInputs == null)
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Gameplay.SetCallbacks(this);
            _playerInputs.PauseMenu.SetCallbacks(this);
            _playerInputs.PlayerMenu.SetCallbacks(this);
            _playerInputs.ShopMenu.SetCallbacks(this);
        }
        _playerInputs.Gameplay.Enable();
        _playerInputs.PauseMenu.Disable();
        _playerInputs.PlayerMenu.Disable();
        _playerInputs.ShopMenu.Disable();
    }
    private void OnDisable()
    {
        _playerInputs.Gameplay.Disable();
        _playerInputs.PauseMenu.Disable();
        _playerInputs.PlayerMenu.Disable();
        _playerInputs.ShopMenu.Disable();
    }
    private void OnDestroy()
    {
        _playerInputs.Gameplay.RemoveCallbacks(this);
        _playerInputs.PlayerMenu.RemoveCallbacks(this);
        _playerInputs.PauseMenu.RemoveCallbacks(this);
        _playerInputs.ShopMenu.RemoveCallbacks(this);
        _playerInputs = null;
        _instance = null;
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

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if(context.started) OnSkill1Event?.Invoke();
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.started) OnSkill2Event?.Invoke();
    }

    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.started) OnSkill3Event?.Invoke();
    }

    public void OnShop(InputAction.CallbackContext context)
    {
        if (context.started){ 
            OnShopEvent?.Invoke();
            _playerInputs.Gameplay.Disable();
            _playerInputs.ShopMenu.Enable();
        }
    }

    public void OnCloseShop(InputAction.CallbackContext context)
    {
        if (context.started){
            OnCloseShopEvent?.Invoke();
            _playerInputs.Gameplay.Enable();
            _playerInputs.ShopMenu.Disable();
        }
    }
    public void OnItem1Event(InputAction.CallbackContext context)
    {
        if(context.started){
            OnItem1Event?.Invoke();
        }
    }
    public void OnItem2Event(InputAction.CallbackContext context)
    {
        if(context.started){
            OnItem1Event?.Invoke();
        }
    }
    public void OnItem3Event(InputAction.CallbackContext context)
    {
        if(context.started){
            OnItem1Event?.Invoke();
        }
    }
    public void OnItem4Event(InputAction.CallbackContext context)
    {
        if(context.started){
            OnItem1Event?.Invoke();
        }
    }
}
