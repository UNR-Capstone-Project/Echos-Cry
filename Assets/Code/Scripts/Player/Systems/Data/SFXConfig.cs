using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/SFX/SFX Config")]
public class SFXConfig : ScriptableObject
{
    [SerializeField] private soundEffect _footstepSFX;
    [SerializeField] private soundEffect _dashSFX;
    [SerializeField] private soundEffect _hurtEffect;
    [SerializeField] private soundEffect _healEffect;
    public soundEffect FootstepSFX { get { return _footstepSFX; } }
    public soundEffect DashSFX { get => _dashSFX; }
    public soundEffect HurtEffect { get => _hurtEffect; }
    public soundEffect HealEffect { get => _healEffect; }
}
