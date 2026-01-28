using UnityEngine;
using UnityEngine.InputSystem;

public class DoorEntrance : DoorManager
{
    [SerializeField] private LevelManager.LevelName levelReferenceName;

    protected override void Start()
    {
        base.Start();
        isLocked = LevelManager.Instance.GetLevelLockedStatus(levelReferenceName);
    }
}
