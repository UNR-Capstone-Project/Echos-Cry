using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] Enemy _enemy;
    [SerializeField] EnemyStateCache.EnemyStates _staggerState;
    public virtual void Execute(float amount)
    {
        _enemy.Stats.Damage(amount, Color.red);
        DamageLabelManager.Instance.SpawnPopup(amount, _enemy.transform.position, Color.white);
        if(!_enemy.Stats.HasArmor) 
            _enemy.StateMachine.SwitchState(_enemy.StateCache.RequestState(_staggerState));
        
    }
}
