using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//New Implementation of enemy behaviors
//Same as previous system but allows for more customizability for enemy states

//Instead of implementing logic for enemies in hard-coded functions that already exist and prevent customizability for new states,
//This system will just allow you to implement individual states yourself

//Keep enemy states for one enemy in a single script file as shown below

//USE THIS SCRIPT AS A REFERENCE AND GUIDE ON HOW IT WORKS

//NOTE: CheckSwitchState is called automatically in the StateMachine so dont worry about calling it individually

public class WalkerSpawnState : EnemyState
{
    public WalkerSpawnState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerIdle));
    }
}

public class WalkerIdleState : EnemyState
{
    private WalkerData _config;

    public WalkerIdleState(WalkerData config, Enemy enemyContext) : base(enemyContext)
    {
        _config = config;
    }
    protected override void OnEnable()
    {
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;
    }
    protected override void OnDisable()
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

    protected override void OnEnter()
    {
        _enemyContext.NPCAnimator.PlayAnimation(_config.IdleHashCode);
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }

    protected override void OnTick()
    {
        CheckPlayerDistance();
    }
    public  void CheckPlayerDistance()
    {
        float playerDistance = Vector3.Distance(PlayerRef.Transform.position, _enemyContext.transform.position);
        if (playerDistance < _config.DistanceCheck)
        {
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerChase));
            return;
        }
    }
}

public class WalkerChaseState : EnemyState
{
    WalkerData _data;
    public WalkerChaseState(WalkerData data,Enemy enemyContext) : base(enemyContext) 
    {
        _data = data;
    }
    protected override void OnEnable()
    {
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;
    }
    protected override void OnDisable()
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

    protected override void OnEnter()
    {
        _enemyContext.NavMeshAgent.stoppingDistance = _data.StoppingDistance;
        _enemyContext.NPCAnimator.PlayAnimation(_data.WalkHashCode);
        SetEnemyTarget();
    }
    protected override void OnExit()
    {
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }
    protected override void OnTick()
    {
        CheckNavMeshDistance();
        SetEnemyTarget();
    }
    public override void Update()
    {
        _enemyContext.NPCAnimator.UpdateSpriteDirection(_enemyContext.NavMeshAgent.velocity, false);
    }

    private void CheckNavMeshDistance()
    {
        NavMeshAgent agent = _enemyContext.NavMeshAgent;
        if (agent == null) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) 
                _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerJump));
        }
    }
    private void SetEnemyTarget()
    {
        if(_enemyContext.NavMeshAgent == null) return;
        _enemyContext.NavMeshAgent.SetDestination(PlayerRef.Transform.position);
    }
}

public class WalkerJumpState : EnemyState
{
    WalkerData _config;

    public WalkerJumpState(WalkerData config, Enemy enemyContext) : base(enemyContext)
    {
        _config = config;
    }

    protected override void OnEnter()
    {
        Vector3 attackDirection = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        attackDirection.y = 0;
        _enemyContext.Rigidbody.isKinematic = false;
        _enemyContext.Rigidbody.AddForce(_config.JumpDashForce * attackDirection, ForceMode.Impulse);
        _enemyContext.NPCAnimator.PlayAnimation(_config.AttackHashCode);
        _enemyContext.SoundStrategy.Execute(_enemyContext.SoundConfig.AttackSFX, _enemyContext.transform, 0);
        _enemyContext.StartCoroutine(JumpDuration());
    }
    protected override void OnExit()
    {
        //Debug.Log("Exit Charge Attack State");
        _enemyContext.Rigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }
    public override void Update()
    {
        _enemyContext.NPCAnimator.UpdateSpriteDirection((PlayerRef.Transform.position - _enemyContext.transform.position).normalized, false);
    }
    private IEnumerator JumpDuration()
    {
        yield return new WaitForSeconds(_config.JumpDuration);
        if (!TempoConductor.Instance.IsOnBeat())
            _enemyContext.StartCoroutine(WaitUntilBeat());
        else
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerAttack));
    }

    IEnumerator WaitUntilBeat()
    {
        yield return new WaitForEndOfFrame();
        if (TempoConductor.Instance.IsOnBeat()) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerAttack));
        else 
            _enemyContext.StartCoroutine(WaitUntilBeat());
    }
}

public class WalkerAttackState : EnemyState
{
    WalkerData _config;
    ParticleSystem _fireRingReference;
    public WalkerAttackState(WalkerData config, Enemy enemyContext) : base(enemyContext) 
    { 
        _config = config;
    }

    protected override void OnEnter()
    {
        GameObject fireRing = GameObject.Instantiate(_config.FireRingPrefab);
        fireRing.transform.position = _enemyContext.transform.position;
        if(fireRing.TryGetComponent<ParticleSystem>(out ParticleSystem particles))
        {
            _fireRingReference = particles;
            particles.Play();
        }
        _enemyContext.AttackStrategies[0].Execute(10f, Vector3.zero, _enemyContext.transform);
        _enemyContext.StartCoroutine(AOEVisualDuration());
        _enemyContext.StartCoroutine(AttackCooldown());
    }
    public override void Update()
    {
        _enemyContext.NPCAnimator.UpdateSpriteDirection((PlayerRef.Transform.position - _enemyContext.transform.position).normalized, false);
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }

    private IEnumerator AOEVisualDuration()
    {
        yield return new WaitForSeconds(_config.FireRingTime);
        _fireRingReference.Stop();
        GameObject.Destroy(_fireRingReference.gameObject, _config.FireRingTime);
    }
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_config.AttackCooldown);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerChase));
    }
}
public class WalkerStaggerState : EnemyState
{
    WalkerData _config;

    public WalkerStaggerState(WalkerData config, Enemy enemyContext) : base(enemyContext)
    {
        _config = config;
    }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Stagger State");
        _enemyContext.NPCAnimator.PlayAnimation(_config.IdleHashCode);
        _enemyContext.NPCAnimator.PlayVisualEffect();
        _enemyContext.Rigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(-(_config.KnockbackForce * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDuration());
    }
    protected override void OnExit()
    {
        _enemyContext.Rigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerDeath));
    }

    private IEnumerator StaggerDuration()
    {
        yield return new WaitForSeconds(_config.StaggerDuration);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.WalkerChase));
    }
}
public class WalkerDeathState : EnemyState
{
    public WalkerDeathState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Death State");
        _enemyContext.DropsStrategy.Execute(_enemyContext.transform);
        Object.Destroy(_enemyContext.gameObject);       
    }
}