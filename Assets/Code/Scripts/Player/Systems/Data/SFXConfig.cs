using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/SFX/SFX Config")]
public class SFXConfig : ScriptableObject
{
    [SerializeField] private soundEffect _footstepSFX;
    [SerializeField] private soundEffect _dashSFX;
    [SerializeField] private soundEffect _excellentHitSFX;
    [SerializeField] private soundEffect _goodHitSFX;
    [SerializeField] private soundEffect _missHitSFX;
    [SerializeField] private soundEffect _hurtEffect;
    [SerializeField] private soundEffect _healEffect;
    public soundEffect FootstepSFX { get { return _footstepSFX; } }
    public soundEffect DashSFX { get => _dashSFX; }
    public soundEffect ExcellentHitSFX { get => _excellentHitSFX; }
    public soundEffect GoodHitSFX { get => _goodHitSFX; }
    public soundEffect MissHitSFX { get => _missHitSFX; }
    public soundEffect HurtEffect { get => _hurtEffect; }
    public soundEffect HealEffect { get => _healEffect; }
}
