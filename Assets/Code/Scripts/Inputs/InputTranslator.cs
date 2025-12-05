using System;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class InputTranslator : MonoBehaviour, 
    PlayerInputs.IGameplayActions, 
    PlayerInputs.IPauseMenuActions, 
    PlayerInputs.IPlayerMenuActions, 
    PlayerInputs.IShopMenuActions
{
    private IEnumerator WaitForSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _inputCount = 0;
        }
    }
    private IEnumerator SpamCooldown()
    {
        _pauseBeatInputs = true;
        yield return new WaitForSeconds(_spamCooldown);
        _pauseBeatInputs = false;
    }
    private void UpdateBPMInputCount()
    {
        float timeBetweenBeats = 60f / (float)MusicManager.Instance.GetTempo();
        _maxInputCountPerSec = (int)(1f/timeBetweenBeats) + _inputPaddingGrace;
    }

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

        _inputCount = 0;
        _pauseBeatInputs= false;

        UpdateBPMInputCount();

        MusicManager.Instance.SongPlayEvent += UpdateBPMInputCount;

        StartCoroutine(WaitForSecond());
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
        MusicManager.Instance.SongPlayEvent -= UpdateBPMInputCount;

        _playerInputs.Gameplay.RemoveCallbacks(this);
        _playerInputs.PlayerMenu.RemoveCallbacks(this);
        _playerInputs.PauseMenu.RemoveCallbacks(this);
        _playerInputs.ShopMenu.RemoveCallbacks(this);
        _playerInputs = null;
        _instance = null;
    }

    private void Update()
    {
        if (_inputCount > _maxInputCountPerSec && !_pauseBeatInputs)
        {
            StartCoroutine(SpamCooldown());
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        OnMovementEvent?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started && !_pauseBeatInputs)
        {
            OnDashEvent?.Invoke();
            _inputCount++;
        }
    }
    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started && !_pauseBeatInputs)
        {
            OnLightAttackEvent?.Invoke();
            _inputCount++;
        }
    }
    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started && !_pauseBeatInputs)
        {
            OnHeavyAttackEvent?.Invoke();
            _inputCount++;
        }
    }
    
    //Code Section Begins. Code Author: Victor
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
    //Code Section Ends.

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
        if(context.started)
        {
            OnShopLeftInput?.Invoke();
        }
    }
    public void OnRightArrow(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnShopRightInput?.Invoke();
        }
    }
    public void OnUpArrow(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnShopUpInput?.Invoke();
        }
    }
    public void OnDownArrow(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            OnShopDownInput?.Invoke();
        }
    }
    public void OnPurchase(InputAction.CallbackContext context)
    {
        if(context.started){
            OnPurchaseEvent?.Invoke();
        }
    }
    public void OnItem1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnItem1Event?.Invoke();
        }
    }
    public void OnItem2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnItem1Event?.Invoke();
        }
    }
    public void OnItem3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnItem1Event?.Invoke();
        }
    }
    public void OnItem4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OnItem1Event?.Invoke();
        }
    }
    public void OnInteract(InputAction.CallbackContext context)
    {
        if(context.started) OnInteractEvent?.Invoke();
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        if(context.started) OnSelectEvent?.Invoke();
    }

    private PlayerInputs _playerInputs;
    public PlayerInputs PlayerInputs { get { return _playerInputs; } }  

    private static InputTranslator _instance;
    public static InputTranslator Instance {  get { return _instance; } }

    public static event Action<Vector2> OnMovementEvent;
    public static event Action          OnDashEvent;
    public static event Action          OnLightAttackEvent;
    public static event Action          OnHeavyAttackEvent;
    public static event Action          OnPauseEvent, OnPauseDownInput, OnPauseUpInput, OnSelectEvent;
    public static event Action          OnResumeEvent;
    public static event Action          OnMapEvent;
    public static event Action          OnExitMapEvent, OnJournalLeftInput, OnJournalRightInput;
    public static event Action          OnSkill1Event, OnSkill2Event, OnSkill3Event;
    public static event Action          OnCloseShopEvent, OnShopLeftInput, OnShopRightInput, OnShopUpInput, OnShopDownInput, OnPurchaseEvent;
    public static event Action          OnItem1Event, OnItem2Event, OnItem3Event, OnItem4Event;
    public static event Action          OnInteractEvent;

    private int _inputCount = 0;
    private int _maxInputCountPerSec = 1;
    [SerializeField] int _inputPaddingGrace = 4;

    private bool _pauseBeatInputs = false;
    [SerializeField] private float _spamCooldown = 5f;
}
