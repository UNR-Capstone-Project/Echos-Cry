using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerComboMeter _playerComboMeter;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerSound _playerSound;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ComboTree _comboStateMachine;
    [SerializeField] private WeaponHandler _playerAttackHandler;
    [SerializeField] private PlayerSkillManager _playerSkillManager;
    [SerializeField] private PlayerDirection _playerDirection;
    [SerializeField] private PlayerCurrencySystem _playerCurrencySystem;
    [SerializeField] private InputTranslator _inputTranslator;

    private PlayerStateMachine _playerStateMachine;
    private PlayerStateCache _playerStateCache;

    public PlayerStats PlayerStats { get => _playerStats; }
    public PlayerComboMeter PlayerComboMeter { get => _playerComboMeter; }
    public PlayerAnimator PlayerAnimator { get => _playerAnimator; }
    public PlayerSound PlayerSound { get => _playerSound; }
    public PlayerMovement PlayerMovement { get => _playerMovement; }
    public ComboTree ComboStateMachine { get => _comboStateMachine; }
    public WeaponHandler PlayerAttackHandler { get => _playerAttackHandler; }
    public PlayerSkillManager PlayerSkillManager { get => _playerSkillManager; }
    public PlayerDirection PlayerDirection { get => _playerDirection; }
    public PlayerCurrencySystem PlayerCurrencySystem { get => _playerCurrencySystem; }
    public InputTranslator InputTranslator { get => _inputTranslator; }

    private void InitStateCache()
    {
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Idle,
            new PlayerIdleState
            (
                this,
                _playerStateMachine,
                _playerStateCache
            )
        );
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Move,
            new PlayerMoveState
            (
                this,
                _playerStateMachine,
                _playerStateCache
            )
        );
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Attack,
            new PlayerAttackState
            (
                this,
                _playerStateMachine,
                _playerStateCache
            )
        );
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Dash,
            new PlayerDashState
            (
                this,
                _playerStateMachine,
                _playerStateCache
            )
        );
    }

    private void Awake()
    {
        _playerStateMachine = new();
        _playerStateCache = new();
    }
    private void Start()
    {
        InitStateCache();
        _playerStateMachine.BindInputs(_inputTranslator);
        _playerStateMachine.Init(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
    }
    private void OnDestroy()
    {
        _playerStateMachine.UnbindInputs(_inputTranslator);
    }
    private void Update()
    {
        _playerStateMachine.UpdateState();
    }
    private void FixedUpdate()
    {
        _playerStateMachine.FixedUpdateState();
    }
}
