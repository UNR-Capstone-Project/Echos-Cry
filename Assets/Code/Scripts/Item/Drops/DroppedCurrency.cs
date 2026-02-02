using Unity.VisualScripting;
using UnityEngine;

public class DroppedCurrency : ItemDropHandler
{
    private PlayerCurrencySystem _currencySystem;
    protected override void OnInteraction(Collider other)
    {
        if (_currencySystem == null)
        {
            if(other.TryGetComponent<Player>(out Player player))
            {
                _currencySystem = player.CurrencySystem; 
            }
        }
        _currencySystem.IncrementGoldCurrency(1);
    }
}
