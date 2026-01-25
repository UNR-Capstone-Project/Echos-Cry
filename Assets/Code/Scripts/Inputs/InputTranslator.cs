using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "Echo's Cry/Input System/Input Translator")]

//ISSUE: This system needs to handle updating and sending invoke for when bindings are changed in settings menu, this updates supporting tool tips to know what the current binding is.

public class InputTranslator : ScriptableObject, 
    PlayerInputs.IGameplayActions, 
    PlayerInputs.IPauseMenuActions, 
    PlayerInputs.IPlayerMenuActions, 
    PlayerInputs.IShopMenuActions
{
    private void Awake()
    {
        _playerInputs = new PlayerInputs();
        _playerInputs.Gameplay.SetCallbacks(this);
        _playerInputs.PauseMenu.SetCallbacks(this);
        _playerInputs.PlayerMenu.SetCallbacks(this);
        _playerInputs.ShopMenu.SetCallbacks(this);
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
        _playerInputs.Gameplay.Disable();
        _playerInputs.PauseMenu.Disable();
        _playerInputs.PlayerMenu.Disable();
        _playerInputs.ShopMenu.Disable();

        _playerInputs.Gameplay.RemoveCallbacks(this);
        _playerInputs.PlayerMenu.RemoveCallbacks(this);
        _playerInputs.PauseMenu.RemoveCallbacks(this);
        _playerInputs.ShopMenu.RemoveCallbacks(this);
        _playerInputs = null;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        OnMovementEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started) OnDashEvent?.Invoke(true);
        else if (context.canceled) OnDashEvent?.Invoke(false);
    }
    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started) OnPrimaryActionEvent?.Invoke(true);
        else if(context.canceled) OnPrimaryActionEvent?.Invoke(false);
    }
    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started) OnSecondaryActionEvent?.Invoke(true);
        else if (context.canceled) OnSecondaryActionEvent?.Invoke(false);
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
        if (context.started) OnPauseUpInput?.Invoke();
    }
    public void OnNavDown(InputAction.CallbackContext context)
    {
        if (context.started) OnPauseDownInput?.Invoke();
    }
    public void OnNavRight(InputAction.CallbackContext context)
    {
        if (context.started) OnJournalRightInput?.Invoke();
    }
    public void OnNavLeft(InputAction.CallbackContext context)
    {
        if (context.started) OnJournalLeftInput?.Invoke();
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        if(context.started) OnSkill1Event?.Invoke(true);
        else if (context.canceled) OnSkill1Event?.Invoke(false);  
    }
    public void OnSkill2(InputAction.CallbackContext context)
    {
        if (context.started) OnSkill2Event?.Invoke(true);
        else if (context.canceled) OnSkill2Event?.Invoke(false);
    }
    public void OnSkill3(InputAction.CallbackContext context)
    {
        if (context.started) OnSkill3Event?.Invoke(true);
        else if (context.canceled) OnSkill3Event?.Invoke(false);
    }

    public void OnCloseShop(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnCloseShopEvent?.Invoke();
            _playerInputs.Gameplay.Enable();
            _playerInputs.ShopMenu.Disable();
        }
    }
    public void OnLeftArrow(InputAction.CallbackContext context)
    {
        if(context.started) OnShopLeftInput?.Invoke();
    }
    public void OnRightArrow(InputAction.CallbackContext context)
    {
        if(context.started) OnShopRightInput?.Invoke();
    }
    public void OnUpArrow(InputAction.CallbackContext context)
    {
        if(context.started) OnShopUpInput?.Invoke();
    }
    public void OnDownArrow(InputAction.CallbackContext context)
    {
        if(context.started) OnShopDownInput?.Invoke();
    }
    public void OnPurchase(InputAction.CallbackContext context)
    {
        if(context.started) OnPurchaseEvent?.Invoke();
    }
    public void OnItem1(InputAction.CallbackContext context)
    {
        if (context.started) OnItem1Event?.Invoke();
        else if(context.canceled) OnItem1Event?.Invoke();
    }
    public void OnItem2(InputAction.CallbackContext context)
    {
        if (context.started) OnItem2Event?.Invoke();
        else if (context.canceled) OnItem2Event?.Invoke();
    }
    public void OnItem3(InputAction.CallbackContext context)
    {
        if (context.started) OnItem3Event?.Invoke();
        else if (context.canceled) OnItem3Event?.Invoke();
    }
    public void OnItem4(InputAction.CallbackContext context)
    {
        if (context.started)OnItem4Event?.Invoke();
        else if (context.canceled) OnItem4Event?.Invoke();
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started) OnInteractEvent?.Invoke();
        else if (context.canceled) OnInteractEvent?.Invoke();
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if(context.started) OnSelectEvent?.Invoke();
    }

    public void OnTeleport(InputAction.CallbackContext context)
    {
        if (context.started) OnTeleportEvent?.Invoke(true);
        else if (context.canceled) OnTeleportEvent?.Invoke(false);
    }

    private PlayerInputs _playerInputs;
    public PlayerInputs PlayerInputs { get { return _playerInputs; } }  

    public event Action<Vector2> OnMovementEvent;
    public event Action<bool>    OnDashEvent, OnTeleportEvent;
    public event Action<bool>    OnPrimaryActionEvent;
    public event Action<bool>    OnSecondaryActionEvent;
    public event Action<bool>    OnSkill1Event, OnSkill2Event, OnSkill3Event;
    public event Action    OnInteractEvent;
    public event Action    OnPauseEvent, OnPauseDownInput, OnPauseUpInput, OnSelectEvent;
    public event Action    OnResumeEvent;
    public event Action    OnMapEvent;
    public event Action    OnExitMapEvent, OnJournalLeftInput, OnJournalRightInput;
    public event Action    OnCloseShopEvent, OnShopLeftInput, OnShopRightInput, OnShopUpInput, OnShopDownInput, OnPurchaseEvent;
    public event Action    OnItem1Event, OnItem2Event, OnItem3Event, OnItem4Event;
}