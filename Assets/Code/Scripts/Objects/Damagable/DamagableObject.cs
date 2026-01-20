using AudioSystem;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamageable
{
    [SerializeField] EnvironmentObject environmentObject;
    [SerializeField] soundEffect hitSFX;

    public void Execute(float amount)
    {
        environmentObject.Health -= amount;
        soundEffectManager.Instance.Builder
            .setSound(hitSFX)
            .setSoundPosition(environmentObject.transform.position)
            .ValidateAndPlaySound();
    }
}
