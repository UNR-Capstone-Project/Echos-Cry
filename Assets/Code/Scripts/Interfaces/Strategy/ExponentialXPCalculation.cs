using UnityEngine;
//Exponential equation
[CreateAssetMenu(menuName = "Echo's Cry/Strategies/XP Calculations/ Exponential")]
public class ExponentialXPCalculation : XPCalculationStrategy
{
    [Header("Determines how fast or slowly the progress of the curve takes as player levels up")]
    [Tooltip("Higher values lead to quick progression on curve (quicker difficulty)")]
    [SerializeField] float _rateOfGrowth;

    [Header("Steepness of the curve as player reaches higher levels")]
    [Tooltip("Higher values will lead to extremely steep progression wall")]
    [SerializeField] float _exponent;

    [Header("Base multiplier to increase amount of total XP required")]
    [SerializeField] float _multiplier;
    public override float Execute(PlayerXP stats)
    {
        return (Mathf.Pow(stats.CurrentLevel, _exponent) * _rateOfGrowth) * _multiplier;
    }
}
