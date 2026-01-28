using UnityEngine;

public class NPCDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private Enemy _npc;
    [SerializeField] private EnemyStateCache.EnemyStates _staggerState;

    public virtual void Execute(float amount)
    {
        _npc.Collider.enabled = false;

        _npc.Stats.Damage(amount, Color.red);

        if (_npc.NPCAnimator != null) //Allow for NPCs without animators
        {
            _npc.SoundStrategy.Execute(_npc.SoundConfig.HitSFX, _npc.transform, 0);
            _npc.NPCAnimator.TintFlash(Color.red, 0.2f);
            _npc.NPCAnimator.PlayVisualEffect();
        }

        DamageLabelManager.Instance.SpawnPopup(amount, _npc.transform.position, Color.white);
        
        if(!_npc.Stats.HasArmor) 
            _npc.StateMachine.SwitchState(_npc.StateCache.RequestState(_staggerState));
    }
}
