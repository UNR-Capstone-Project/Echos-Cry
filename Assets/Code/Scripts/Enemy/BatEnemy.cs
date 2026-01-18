using UnityEngine;

public class BatEnemy : Enemy
{
    public override void Init()
    {
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Spawn, 
            new BatSpawnState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Stagger,
            new BatStaggerState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Attack,
            new BatAttackState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Chase,
            new BatChaseState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Death,
            new BatDeathState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Idle,
            new BatIdleState(this)
        );
        _stateCache.AddState(
            EnemyStateCache.EnemyStates.Bat_Charge,
            new BatChargeState(this)
        );

        _stateMachine.Init(_stateCache.RequestState(EnemyStateCache.EnemyStates.Bat_Spawn));
    }
}
