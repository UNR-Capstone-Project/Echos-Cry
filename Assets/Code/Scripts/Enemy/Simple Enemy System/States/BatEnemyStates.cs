using NUnit.Framework.Constraints;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

//New Implementation of enemy behaviors
//Same as previous system but allows for more customizability for enemy states

//Instead of implementing logic for enemies in hard-coded functions that already exist and prevent customizability for new states,
//This system will just allow you to implement individual states yourself

//Keep enemy states for one enemy in a single script file as shown below

//USE THIS SCRIPT AS A REFERENCE AND GUIDE ON HOW IT WORKS

//NOTE: CheckSwitchState is called automatically in the StateMachine so dont worry about calling it individually

public class BatSpawnState : EnemyState
{
    public BatSpawnState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Spawn");
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Idle));
    }
}

public class BatIdleState : EnemyState
{
    private readonly BatEnemyConfigFile _configFile;

    private void CheckPlayerDistance()
    {
        if (PlayerRef.Transform == null) return;
        float playerDistance = Vector3.Distance(_enemyContext.transform.position, PlayerRef.Transform.position);
        if (playerDistance < _configFile.DistanceCheck)
        {
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Chase));
            return;
        }
    }

    public BatIdleState(BatEnemyConfigFile configFile, Enemy enemyContext) : base(enemyContext)
    {
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;

        _configFile = configFile;
    }
    ~BatIdleState()
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

    public override void Tick()
    {
        if (!_isActive) return;
        CheckPlayerDistance();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Death));
    }
}

public class BatChaseState : EnemyState
{
    private void CheckNavMeshDistance()
    {
        NavMeshAgent agent = _enemyContext.NavMeshAgent;
        if (agent == null) return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Charge));
        }
    }
    private void SetEnemyTarget()
    {
        if(_enemyContext.NavMeshAgent == null || PlayerRef.Transform == null) return;
        _enemyContext.NavMeshAgent.SetDestination(PlayerRef.Transform.position);
    }
 
    public BatChaseState(Enemy enemyContext) : base(enemyContext) 
    {
        TickManager.Instance.GetTimer(0.2f).Tick += Tick;
    }
    ~BatChaseState()
    {
        TickManager.Instance.GetTimer(0.2f).Tick -= Tick;
    }

    protected override void OnEnter()
    {
        base.OnEnter();
        //Debug.Log("Enter Chase");
        SetEnemyTarget();
    }
    protected override void OnExit()
    {
        base.OnExit();
        //Debug.Log("Exit Chase");
        _enemyContext.StopAllCoroutines();
    }
    public override void Tick()
    {
        if (!_isActive) return;
        CheckNavMeshDistance();
        SetEnemyTarget();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Death));
    }
}

public class BatChargeState : EnemyState
{
    private readonly BatEnemyConfigFile _configFile;
    private IEnumerator ChargeAttackCoroutine()
    {
        yield return new WaitForSeconds(_configFile.AttackChargeTime);
        if (TempoConductor.Instance.IsOnBeat()) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Attack));
        else 
            _enemyContext.StartCoroutine(WaitUntilBeat());
    }
    private IEnumerator WaitUntilBeat()
    {
        yield return new WaitForEndOfFrame();
        if (TempoConductor.Instance.IsOnBeat()) 
            _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Attack));
        else 
            _enemyContext.StartCoroutine(WaitUntilBeat());
    }

    public BatChargeState(BatEnemyConfigFile configFile,Enemy enemyContext) : base(enemyContext) 
    {
        _configFile = configFile;
    }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Charge Attack State");
        _enemyContext.StartCoroutine(ChargeAttackCoroutine());
    }
    protected override void OnExit()
    {
        //Debug.Log("Exit Charge Attack State");
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Death));
    }
}

public class BatAttackState : EnemyState
{
    private readonly BatEnemyConfigFile _configFile;
    private Vector3 attackDirection;
    private enum AttackStrats
    {
        Melee = 0,
    }
    private bool isAttacking;

    private int MeleeIndex => (int)AttackStrats.Melee;
    private IEnumerator AttackDuration()
    {
        isAttacking = true;
        yield return new WaitForSeconds(_configFile.AttackDuration);
        isAttacking = false;
        _enemyContext.StartCoroutine(AttackCooldown());
    }
    private IEnumerator AttackCooldown()
    {

        yield return new WaitForSeconds(_configFile.AttackCooldown);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Chase));
    }

    public BatAttackState(BatEnemyConfigFile configFile, Enemy enemyContext) : base(enemyContext) 
    {
        _configFile = configFile;
    }

    protected override void OnEnter()
    {
        isAttacking = true;

        _enemyContext.Rigidbody.isKinematic = false;
        attackDirection = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(_configFile.AttackDashForce * attackDirection, ForceMode.Impulse);
        _enemyContext.Animator.Play("Attack");
        _enemyContext.StartCoroutine(AttackDuration());
    }
    public override void Update()
    {
        if (!isAttacking 
            || _enemyContext.AttackStrategies.Length == 0 
            || _enemyContext.AttackStrategies[MeleeIndex] == null) 
            return;

        if (_enemyContext.AttackStrategies[MeleeIndex]
            .Execute(
                10f, 
                attackDirection, 
                _enemyContext.transform)) 
            isAttacking = false;
    }
    protected override void OnExit()
    {
        _enemyContext.Rigidbody.isKinematic = true;
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Death));
    }

}

public class BatStaggerState : EnemyState
{
    private readonly BatEnemyConfigFile _configFile;
    private IEnumerator StaggerDuration()
    {
        yield return new WaitForSeconds(_configFile.StaggerDuration);
        _enemyContext.StateMachine.SwitchState(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Chase));
    }

    public BatStaggerState(BatEnemyConfigFile configFile, Enemy enemyContext) : base(enemyContext)
    {
        _configFile = configFile;
    }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Stagger State");
        _enemyContext.Rigidbody.isKinematic = false;
        Vector3 direction = (PlayerRef.Transform.position - _enemyContext.transform.position).normalized;
        _enemyContext.Rigidbody.AddForce(-(_configFile.KnockbackForce * direction), ForceMode.Impulse);
        _enemyContext.StartCoroutine(StaggerDuration());
    }
    protected override void OnExit()
    {
        _enemyContext.Rigidbody.isKinematic = true;
        _enemyContext.StopAllCoroutines();
    }
    public override void CheckSwitch()
    {
        CheckDeath(_enemyContext.StateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Death));
    }
}
public class BatDeathState : EnemyState
{
    public BatDeathState(Enemy enemyContext) : base(enemyContext) { }

    protected override void OnEnter()
    {
        //Debug.Log("Enter Death State");
        //_enemyContext.Stats.HandleEnemyDeath();
        _enemyContext.DropsStrategy.Execute(_enemyContext.transform);
        //Temporary
        Object.Destroy(_enemyContext.gameObject);
    }
}