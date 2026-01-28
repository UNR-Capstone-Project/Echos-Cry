using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]
public struct Level
{
    public LevelManager.LevelName name;
    public bool locked;
}

public class LevelManager : MonoBehaviour
{
    private static LevelManager _instance;
    public static LevelManager Instance { get { return _instance; } }

    public static event Action<LevelName> LevelUnlockedEvent;
    public static event Action<LevelName> LevelLockEvent;

    public enum LevelName
    {
        LEVEL_ONE = 0,
        LEVEL_TWO = 1
    }

    [SerializeField] private List<Level> LevelList = new();

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }

    private void Start()
    {
        PlayerStats.OnPlayerDeathEvent += LockAllLevels;
    }
    private void OnDestroy()
    {
        PlayerStats.OnPlayerDeathEvent -= LockAllLevels;
    }

    public void UnlockLevel(LevelName levelName)
    {
        int index = (int)levelName;
        if (index >= LevelList.Count) { return; }

        for (int i = 0; i < LevelList.Count; i++)
        {
            if (LevelList[i].name == levelName)
            {
                Level newLevel = LevelList[i];
                newLevel.locked = false;
                LevelList[i] = newLevel;
                break;
            }
        }

        LevelUnlockedEvent?.Invoke(levelName);
    }

    public void LockAllLevels()
    {
        for (int i = 1; i < LevelList.Count; i++) //Skip locking the first level!
        {
            Level newLevel = LevelList[i];
            newLevel.locked = true;
            LevelList[i] = newLevel;
            LevelUnlockedEvent?.Invoke(LevelList[i].name);
        }
    }

    public bool GetLevelLockedStatus(LevelName levelName)
    {
        for (int i = 0; i < LevelList.Count; i++)
        {
            if (LevelList[i].name == levelName)
            {
                return LevelList[i].locked;
            }
        }
        return false;
    }
}
