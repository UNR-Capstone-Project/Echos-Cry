using UnityEngine;
//Exponential equation
[CreateAssetMenu(menuName = "Echo's Cry/Strategies/XP Calculations/ Exponential")]
public class ExponentialXPCalculation : XPCalculationStrategy
{
    [SerializeField] float _baseXP;
    [SerializeField] float _growthRate;

    public override float Execute(PlayerXP stats)
    {
        return Mathf.Floor(_baseXP * Mathf.Pow(1 + _growthRate, stats.CurrentLevel));
    }
}
