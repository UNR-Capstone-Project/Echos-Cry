using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Weapon/Combo Data")]
public class ComboWeaponData : ScriptableObject
{
    [SerializeField] private AttackData[] _attackData;
    [SerializeField] private float _comboResetTime;
    [SerializeField] private float _secondarySoundDelay;

    public AttackData[] AttackData { get => _attackData; }
    public float ComboResetTime { get => _comboResetTime; }
    public float SecondarySoundDelay { get => _secondarySoundDelay; }
}