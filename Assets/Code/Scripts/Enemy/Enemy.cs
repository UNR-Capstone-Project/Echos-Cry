using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class Enemy : MonoBehaviour
{
    private EnemyStateCache _stateCache;
    private EnemyStateMachine _stateMachine;

    [Header("Enemy-Related Components")]
    [SerializeField] private EnemyStats _stats;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private EnemySoundConfig _soundConfig;
    [SerializeField] private Collider _collider;
    [SerializeField] private VisualEffect _visualEffects;

    [Header("Strategies")]
    [SerializeField] private AttackStrategy[] _attackStrats;
    [SerializeField] private TargetStrategy[]   _targetStrats;
    [SerializeField] private MovementStrategy[] _movementStrats;
    [SerializeField] private ItemDropStrategy _drops;
    [SerializeField] private SoundStrategy _soundStrategy;
    [SerializeField] private EnemyCacheStrategy _enemyCacheStrategy;

    public EnemyStateCache StateCache { get => _stateCache; }
    public EnemyStateMachine StateMachine { get => _stateMachine; }

    public EnemyStats Stats { get => _stats; }
    public NavMeshAgent NavMeshAgent { get => _navMeshAgent; }
    public Rigidbody Rigidbody { get => _rigidbody; }
    public Animator Animator { get => _animator; }
    public EnemySoundConfig SoundConfig { get => _soundConfig; }
    public Collider Collider { get => _collider; }

    public AttackStrategy[] AttackStrategies { get => _attackStrats; }
    public TargetStrategy[] TargetStrategy { get => _targetStrats; }
    public MovementStrategy[] MovementStrategy { get => _movementStrats; }
    public ItemDropStrategy DropsStrategy { get => _drops; }
    public SoundStrategy SoundStrategy { get => _soundStrategy; }
    public VisualEffect VisualEffects { get => _visualEffects; }

    protected virtual void Awake()
    {   
        _stateMachine = new();
        _stateCache = new();
        _enemyCacheStrategy.Execute(_stateCache, this);
        _stateMachine.Init(_stateCache.StartState);
    }
    private void Start()
    {
        Player.AttackEndedEvent += ResetCollider;
    }
    private void OnDestroy()
    {
        Player.AttackEndedEvent -= ResetCollider;
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