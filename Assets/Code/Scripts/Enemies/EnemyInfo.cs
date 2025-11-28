using UnityEngine;
using UnityEngine.AI;

//Stores information relevant to the enemy, specifically necessary ID's

[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator))]
public class EnemyInfo : MonoBehaviour
{
    //EnemyID is a non-persistent ID of the current enemy that represents its position in the EnemyPool and EnemyStatsPool
    public int EnemyID;
}
