using UnityEngine;
using UnityEngine.InputSystem;
using static LevelManager;

public class DoorWaveEntrance : DoorManager
{
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private bool isExit = false;
    [SerializeField] private LevelManager.LevelName levelName;

    protected override void Start()
    {
        base.Start();
        if (waveManager != null)
        {
            waveManager.OnAllWavesCompleted += HandleWaveComplete;
            isLocked = true;
        }
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        if (waveManager != null)
        {
            waveManager.OnAllWavesCompleted -= HandleWaveComplete;
        }
    }

    private void HandleWaveComplete()
    {
        isLocked = false;
        if (isExit)
        {
            LevelName nextLevel = (LevelName)((int)levelName + 1); //Unlock the next level.
            LevelManager.Instance.UnlockLevel(nextLevel);
        }
    }
}
