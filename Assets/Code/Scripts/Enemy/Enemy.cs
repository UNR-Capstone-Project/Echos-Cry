using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    private EnemyStateCache _stateCache;
    private EnemyStateMachine _stateMachine;
    private EnemyPool _pool;
    private bool IsPooled => _pool != null;

    [Header("Enemy-Related Components")]
    [SerializeField] private HealthSystem _health;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private NPCAnimator _npcAnimator;
    [SerializeField] private EnemySoundConfig _soundConfig;
    [SerializeField] private Collider _collider;
    [SerializeField] private PassiveEffectHandler _passiveEffectHandler;

    [Header("Strategies")]
    [SerializeField] private AttackStrategy[] _attackStrats;
    [SerializeField] private TargetStrategy[]   _targetStrats;
    [SerializeField] private MovementStrategy[] _movementStrats;
    [SerializeField] private ItemDropStrategy _drops;
    [SerializeField] private SoundStrategy _soundStrategy;
    [SerializeField] private EnemyCacheStrategy _enemyCacheStrategy;

    [Header("Event Channel (Subscriber)")]
    [Tooltip("Invoked when player's attack ends")]
    [SerializeField] private EventChannel _playerAttackEndChannel;

    public event Action OnDeathEvent;
    public void HandleDeath()
    {
        OnDeathEvent?.Invoke();
       // if (IsPooled) _pool.ReleaseEnemy(this);
        //else Destroy(gameObject);
        
    }

    public EnemyStateCache StateCache { get => _stateCache; }
    public EnemyStateMachine StateMachine { get => _stateMachine; }
    public EnemyPool Pool { get => _pool; set => _pool = value; }

    public HealthSystem Health { get => _health; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public NPCAnimator NPCAnimator { get => _npcAnimator; }
    public EnemySoundConfig SoundConfig { get => _soundConfig; }
    public Collider Collider { get => _collider; }
    public PassiveEffectHandler PassiveEffectHandler { get => _passiveEffectHandler; }

    public AttackStrategy[] AttackStrategies { get => _attackStrats; }
    public TargetStrategy[] TargetStrategy { get => _targetStrats; }
    public MovementStrategy[] MovementStrategy { get => _movementStrats; }
    public ItemDropStrategy DropsStrategy { get => _drops; }
    public SoundStrategy SoundStrategy { get => _soundStrategy; }

    protected virtual void Awake()
    {   
        _stateMachine = new();
        _stateCache = new();

        _enemyCacheStrategy.Execute(_stateCache, this);
        _stateMachine.Init(_stateCache.StartState); 
    }
    private void OnEnable()
    {
        _stateCache?.Enable();
        _playerAttackEndChannel.Channel += ResetCollider;
    }
    private void OnDisable()
    {
        _stateCache?.Disable();
        _playerAttackEndChannel.Channel -= ResetCollider; 
    }
    private void OnDestroy()
    {
        _stateMachine = null;
        _stateCache = null;
    }

    protected virtual void Update()
    {
        _stateMachine.UpdateState();
    }

    private void ResetCollider() => _collider.enabled = true;

    public class Builder
    {
        //TODO
    }
}