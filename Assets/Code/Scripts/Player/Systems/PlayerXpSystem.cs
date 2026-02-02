using System;
using UnityEngine;

public class PlayerXpSystem : MonoBehaviour
{
    private int _xpAmount;
    private int _level = 1;
    private int _levelUpXpRequired = 100;
    public int XP { get { return _xpAmount; } }
    public int Level { get { return _level; } }
    public int XpRequired { get { return _levelUpXpRequired; } }

    public static event Action<int, int, int> OnXPChangeEvent;
    public static event Action<int> OnLevelUp;

    private void CheckLevel()
    {
        if (_xpAmount >= _levelUpXpRequired)
        {
            _level++;
            _xpAmount -= _levelUpXpRequired;
            _levelUpXpRequired = (int)Mathf.Floor((float)(_levelUpXpRequired * 1.5));

            OnLevelUp?.Invoke(_level);
        }
    }

    public void IncrementXp(int amount)
    {
        _xpAmount += amount;
        CheckLevel();
        OnXPChangeEvent?.Invoke(_xpAmount, _levelUpXpRequired, _level);
    }

    public void SetXp(int amount)
    {
        _xpAmount = amount;
        CheckLevel();
        OnXPChangeEvent?.Invoke(_xpAmount, _levelUpXpRequired, _level);
    }
}