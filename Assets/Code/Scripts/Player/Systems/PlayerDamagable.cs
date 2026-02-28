using UnityEngine;

public class PlayerDamagable : MonoBehaviour, IDamageable
{
    [SerializeField] Player player;
    public void Execute(float amount)
    {
        player.Health.Damage(amount, Color.white);
        if(player.Health.HasArmor) player.Animator.TintFlash(Color.blue);
        else player.Animator.TintFlash(Color.red);
        CameraManager.Instance.ScreenShake(0.4f, 0.4f);
        player.SFX.Execute(player.SFXConfig.HurtEffect, player.transform, 0);
    }
}
