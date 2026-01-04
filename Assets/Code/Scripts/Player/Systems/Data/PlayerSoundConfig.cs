using AudioSystem;
using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Sound Config")]
public class PlayerSoundConfig : ScriptableObject
{
    [SerializeField] private soundEffect _footstepSFX;
    public soundEffect FootstepSFX { get { return _footstepSFX; } }
}
