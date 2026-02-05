using AudioSystem;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamageable
{
    [SerializeField] EnvironmentObject environmentObject;
    [SerializeField] soundEffect hitSFX;

    public void Execute(float amount)
    {
        environmentObject.Health -= amount;
        SoundEffectManager.Instance.Builder
            .SetSound(hitSFX)
            .SetSoundPosition(environmentObject.transform.position)
            .ValidateAndPlaySound();
    }
}
