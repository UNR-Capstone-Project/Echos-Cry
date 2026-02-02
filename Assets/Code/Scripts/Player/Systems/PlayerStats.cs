using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private int _currentLevel = 1;
    private float _currentXPAmount;
    private float _goalXPAmount = 10f;
    [SerializeField] private XPCalculationStrategy _newXPGoalCalculation;

    public int CurrentLevel { get => _currentLevel; }
    public float CurrentXPAmount { get => _currentXPAmount; }
    public float GoalXPAmount { get => _goalXPAmount; }

    private void Start()
    {
        _goalXPAmount = _newXPGoalCalculation.Execute(this);
    }

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