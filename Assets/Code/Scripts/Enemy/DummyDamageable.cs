using UnityEngine;

public class DummyDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] private Collider _collider;
    [SerializeField] private SoundStrategy _soundStrategy;
    [SerializeField] private NPCAnimator _npcAnimator;
    [SerializeField] private EnemySoundConfig _soundConfig;

    public virtual void Execute(float amount)
    {
        _collider.enabled = false;
        _soundStrategy.Execute(_soundConfig.HitSFX, gameObject.transform, 0);
        _npcAnimator.TintFlash(Color.red, 0.2f);

        DamageLabelManager.Instance.SpawnPopup(amount, gameObject.transform.position, Color.white);
    }
    private void Start()
    {
        Player.AttackEndedEvent += ResetCollider;
    }
    private void OnDestroy()
    {
        Player.AttackEndedEvent -= ResetCollider;
    }
    private void ResetCollider() => _collider.enabled = true;
}