using AudioSystem;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public void HandleDamageSound(float damage, Color color)
    {
        SoundEffectManager.Instance.Builder
            .setSound(hitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    public void HandleAttackSound()
    {
        SoundEffectManager.Instance.Builder
            .setSound(attackSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    void Start()
    {
        GetComponent<EnemyStats>().OnEnemyDamagedEvent += HandleDamageSound;
        GetComponent<EnemyBaseAttack>().OnEnemyUseAttackEvent += HandleAttackSound;
    }
    private void OnDestroy()
    {
        GetComponent<EnemyStats>().OnEnemyDamagedEvent -= HandleDamageSound;
        GetComponent<EnemyBaseAttack>().OnEnemyUseAttackEvent -= HandleAttackSound;
    }
    
    [SerializeField] soundEffect hitSFX;
    [SerializeField] soundEffect attackSFX;
}
