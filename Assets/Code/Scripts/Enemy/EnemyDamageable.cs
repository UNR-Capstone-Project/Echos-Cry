using UnityEngine;

public class EnemyDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private EnemyStateCache.EnemyStates _staggerState;
    public virtual void Execute(float amount)
    {
        _enemy.Collider.enabled = false;
        _enemy.Stats.Damage(amount, Color.red);
        _enemy.SoundStrategy.Execute(_enemy.SoundConfig.HitSFX, _enemy.transform, 0);
        _enemy.VisualEffects.Play();
        DamageLabelManager.Instance.SpawnPopup(amount, _enemy.transform.position, Color.white);
        if(!_enemy.Stats.HasArmor) 
            _enemy.StateMachine.SwitchState(_enemy.StateCache.RequestState(_staggerState));
    }
}
