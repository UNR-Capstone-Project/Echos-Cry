using AudioSystem;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    public void HandleDamageSound(float damage, Color color)
    {
        soundEffectManager.Instance.Builder
            .setSound(hitSFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
    }

    void Start()
    {
        GetComponent<EnemyStats>().OnEnemyDamagedEvent += HandleDamageSound;
    }
    private void OnDestroy()
    {
        GetComponent<EnemyStats>().OnEnemyDamagedEvent -= HandleDamageSound;
    }
    
    [SerializeField] soundEffect hitSFX;
}
