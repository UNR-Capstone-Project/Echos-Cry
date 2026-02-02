using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Relevant Player Components")]
    [SerializeField] private PlayerStats _stats;
    [SerializeField] private PlayerComboMeter _comboMeter;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private WeaponHolder _weaponHolder;
    [SerializeField] private PlayerSkillManager _skills;
    [SerializeField] private PlayerOrientation _orientation;
    [SerializeField] private PlayerCurrencySystem _currencySystem;
    [SerializeField] private PlayerXpSystem _xpSystem;
    [SerializeField] private InputTranslator _inputTranslator;
    [SerializeField] private SoundStrategy _sfx;
    [SerializeField] private SFXConfig _sfxConfig;

    private PlayerStateMachine _playerStateMachine;
    private PlayerStateCache _playerStateCache;
    [Header("Event Channel (Broadcaster)")]
    [SerializeField] EventChannel _attackEndedChannel;
    public void InvokeAttackEnded()
    {
        if (_attackEndedChannel != null) _attackEndedChannel.Invoke();
    }

    public PlayerStats Stats { get => _stats; }
    public PlayerComboMeter ComboMeter { get => _comboMeter; }
    public PlayerAnimator Animator { get => _animator; }
    public SoundStrategy SFX { get => _sfx; }
    public SFXConfig SFXConfig { get => _sfxConfig; }
    public PlayerMovement Movement { get => _movement; }
    public WeaponHolder WeaponHolder { get => _weaponHolder; }
    public PlayerSkillManager Skills { get => _skills; }
    public PlayerOrientation Orientation { get => _orientation; }
    public PlayerCurrencySystem CurrencySystem { get => _currencySystem; }
    public PlayerXpSystem XpSystem { get => _xpSystem; }
    public InputTranslator InputTranslator { get => _inputTranslator; }

    private void InitStateCache()
    {
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Idle,
            new PlayerIdleState (this, _playerStateMachine, _playerStateCache)
        );
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Move,
            new PlayerMoveState
            (this, _playerStateMachine, _playerStateCache)
        );
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Attack,
            new PlayerAttackState
            (this, _playerStateMachine, _playerStateCache)
        );
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Dash,
            new PlayerDashState
            (this, _playerStateMachine, _playerStateCache)
        );
        _playerStateCache.AddState(
            PlayerStateCache.PlayerState.Death,
            new PlayerDeathState
            (this, _playerStateMachine, _playerStateCache)
        );
    }
    public void Reset()
    {
        _playerStateMachine.SwitchState(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
        _stats.ResetHealth();
    }

    private void Awake()
    {
        _playerStateMachine = new();
        _playerStateCache = new();
    }
    private void Start()
    {
        InitStateCache();
        _playerStateMachine.Init(_playerStateCache.RequestState(PlayerStateCache.PlayerState.Idle));
    }
    private void OnEnable()
    {
        _playerStateMachine.BindInputs(_inputTranslator);
    }
    private void OnDisable()
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