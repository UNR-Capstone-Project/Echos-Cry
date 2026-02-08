using UnityEngine;

public class NPCDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy _npc;
    [SerializeField] private EnemyStateCache.EnemyStates _staggerState;

    public virtual void Execute(float amount)
    {
        _npc.Collider.enabled = false;

        amount *= _npc.Health.DamageMultiplier;

        _npc.Health.Damage(amount, Color.red);

        _npc.SoundStrategy.Execute(_npc.SoundConfig.HitSFX, _npc.transform, 0);
        _npc.NPCAnimator.TintFlash(Color.red, 0.2f);
        _npc.NPCAnimator.PlayVisualEffect();

        if(DamageLabelManager.Instance != null)
            DamageLabelManager.Instance.SpawnPopup(amount, _npc.transform.position, Color.white);
        
        if(!_npc.Health.HasArmor) 
            _npc.StateMachine.SwitchState(_npc.StateCache.RequestState(_staggerState));
    }
}

public struct AttackInfo
{
    public float damage;
    public float force;
    public ForceMode forceMode;
    public TempoConductor.HitQuality hitQuality;
    public Vector3 direction;
}
