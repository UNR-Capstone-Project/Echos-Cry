using UnityEngine;

public class PlayerDamagable : MonoBehaviour, IDamageable
{
    [SerializeField] Player player;
    public void Execute(float amount)
    {
        player.Stats.Damage(amount);
        player.Animator.TintFlash(Color.red);
        player.SFX.Execute(player.SFXConfig.HurtEffect, player.transform, 0);
    }
}
