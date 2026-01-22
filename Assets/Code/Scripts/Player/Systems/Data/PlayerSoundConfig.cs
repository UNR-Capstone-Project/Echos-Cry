using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Sound Config")]
public class PlayerSoundConfig : ScriptableObject
{
    [SerializeField] private soundEffect _footstepSFX;
    [SerializeField] private soundEffect _dashSFX;
    [SerializeField] private soundEffect _excellentHitSFX;
    [SerializeField] private soundEffect _goodHitSFX;
    [SerializeField] private soundEffect _missHitSFX;
    public soundEffect FootstepSFX { get { return _footstepSFX; } }
    public soundEffect DashSFX { get => _dashSFX; }
    public soundEffect ExcellentHitSFX { get => _excellentHitSFX; }
    public soundEffect GoodHitSFX { get => _goodHitSFX; }
    public soundEffect MissHitSFX { get => _missHitSFX; }
}
