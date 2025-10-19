using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyStateMachine _enemySM;
    private EnemyStats _enemyStats;
    private void Awake()
    {
        
    }
    private void Start()
    {
        _enemySM.StateMachineStart();
    }
}
