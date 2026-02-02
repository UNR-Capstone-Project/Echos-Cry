using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Currency Config")]
public class PlayerCurrencyConfig : ScriptableObject
{
    [SerializeField] private int _startingGoldCurrency;
    public int StartingGoldCurrency { get { return _startingGoldCurrency; } }
}
