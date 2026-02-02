using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _currentLevel = 1;
    private float _currentXPAmount;
    private float _goalXPAmount;
    [SerializeField] private XPCalculationStrategy _newXPGoalCalculation;

    public int CurrentLevel { get => _currentLevel; }
    public float CurrentXPAmount { get => _currentXPAmount; }
    public float GoalXPAmount { get => _goalXPAmount; }

    public void IncreaseXP(float xp)
    {
        _currentXPAmount += xp;
        if(_currentXPAmount >= _goalXPAmount)
        {
            _currentLevel++;
            //Set new goal amount
            if (_newXPGoalCalculation != null)
                _goalXPAmount = _newXPGoalCalculation.Execute(this);
            else _goalXPAmount = _goalXPAmount + _currentLevel * 1.5f;
            
            _currentXPAmount = 0;
        }
    }
}

public abstract class XPCalculationStrategy : ScriptableObject
{
    public abstract float Execute(PlayerStats stats);
}
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
    public override float Execute(PlayerStats stats)
    {
        return (Mathf.Pow(stats.CurrentLevel, _exponent) * _rateOfGrowth) * _multiplier;
    }
}
