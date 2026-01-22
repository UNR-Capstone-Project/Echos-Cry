using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Currency Config")]
public class PlayerCurrencyConfig : ScriptableObject
{
    [SerializeField] private int _startingFingerCurrency;
    public int StartingFingerCurrency { get { return _startingFingerCurrency; } }
}
