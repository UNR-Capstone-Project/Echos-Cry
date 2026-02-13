using AudioSystem;
using UnityEngine;

public class DroppedXP : ItemDropHandler
{
    [SerializeField] private float _xpAmount = 10f;
    private PlayerStats _stats;
    protected override void OnInteraction(Collider other)
    {
        if(_stats == null)
        {
            if(other.TryGetComponent<Player>(out Player player))
            {
                _stats = player.Stats;
                _stats.IncreaseXP(_xpAmount); //TODO: Track with player level here so it increases.
            }
        }
        else _stats.IncreaseXP(_xpAmount);
    }
}
