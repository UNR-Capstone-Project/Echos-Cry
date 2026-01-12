using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerComboMeter _playerComboMeter;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerSound _playerSound;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ComboStateMachine _comboStateMachine;
    [SerializeField] private PlayerAttackHandler _playerAttackHandler;
    [SerializeField] private PlayerSkillManager _playerSkillManager;
    [SerializeField] private PlayerDirection _playerDirection;
    [SerializeField] private PlayerCurrencySystem _playerCurrencySystem;
    [SerializeField] private InputTranslator _inputTranslator;

    private PlayerStateMachine _playerStateMachine;
    private PlayerStateCache _playerStateCache;
    private PlayerActionInputHandler _playerInputHandler;

    public PlayerStats PlayerStats { get => _playerStats; }
    public PlayerComboMeter PlayerComboMeter { get => _playerComboMeter; }
    public PlayerAnimator PlayerAnimator { get => _playerAnimator; }
    public PlayerSound PlayerSound { get => _playerSound; }
    public PlayerMovement PlayerMovement { get => _playerMovement; }
    public ComboStateMachine ComboStateMachine { get => _comboStateMachine; }
    public PlayerAttackHandler PlayerAttackHandler { get => _playerAttackHandler; }
    public PlayerSkillManager PlayerSkillManager { get => _playerSkillManager; }
    public PlayerDirection PlayerDirection { get => _playerDirection; }
    public PlayerCurrencySystem PlayerCurrencySystem { get => _playerCurrencySystem; }
    public InputTranslator InputTranslator { get => _inputTranslator; }

    private void Awake()
    {
        _playerStateMachine = new PlayerStateMachine();
        _playerStateCache = new PlayerStateCache(this, _playerStateMachine);
        _playerInputHandler = new PlayerActionInputHandler(_inputTranslator, _playerStateMachine);
    }
    private void Start()
    {
        _playerStateMachine.Init(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
        _playerInputHandler.BindEvents();
    }
    private void OnDestroy()
    {
        _playerInputHandler.UnbindEvents();
    }
    private void Update()
    {
        _playerStateMachine.Update();
    }
    private void FixedUpdate()
    {
        _playerStateMachine.FixedUpdate();
    }
}
