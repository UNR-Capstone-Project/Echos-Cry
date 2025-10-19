using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovement
{
    private NavMeshAgent _nmAgent;
    public abstract void MoveEnemy();
}
