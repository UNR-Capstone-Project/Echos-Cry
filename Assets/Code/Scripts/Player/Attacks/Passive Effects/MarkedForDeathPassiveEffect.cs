using UnityEngine;

[CreateAssetMenu(fileName = "Marked For Death Effect", menuName = "Echo's Cry/Passive Effects/Marked For Death")]
public class MarkedForDeathPassive : PassiveEffect
{
    public float damageMultiplier = 1.5f;
    private bool _markedForDeath = false;
    [SerializeField] private GameObject _markedIcon;
}
