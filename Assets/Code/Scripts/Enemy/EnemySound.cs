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
    
    [SerializeField] soundEffect hitSFX;
    [SerializeField] soundEffect attackSFX;
}
