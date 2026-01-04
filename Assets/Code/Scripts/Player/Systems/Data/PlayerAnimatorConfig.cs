using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Animator Config")]
public class PlayerAnimatorConfig : ScriptableObject
{
    [SerializeField] private Color _onPlayerDamagedTintColor;
    [SerializeField] private float _onPlayerDamagedTintFlashDuration;
    public Color OnPlayerDamagedTintColor { get {  return _onPlayerDamagedTintColor;} }
    public float OnPlayerDamagedTintFlashDuration { get { return _onPlayerDamagedTintFlashDuration;} }
}
