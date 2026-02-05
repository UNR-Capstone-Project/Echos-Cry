using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Animator Config")]
public class PlayerAnimatorConfig : ScriptableObject
{
    [SerializeField] private Color _onPlayerDamagedTintColor;
    [SerializeField, Range(0, 1)] private float _tintFlashDuration;
    public Color OnPlayerDamagedTintColor { get {  return _onPlayerDamagedTintColor;} }
    public float TintFlashDuration { get { return _tintFlashDuration;} }
}
