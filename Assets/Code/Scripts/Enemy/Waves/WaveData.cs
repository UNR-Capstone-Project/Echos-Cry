using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
/// 
/// <summary>
/// A ScriptableObject to hold all the data about enemy spawn waves to be used in an area of a level
/// </summary>
[CreateAssetMenu(fileName = "WaveData", menuName = "Echo's Cry/Enemy Wave System/WaveData")]
public class WaveData : ScriptableObject
{
    public int totalEnemies;
    public GameObject[] enemyTypesArray;
    public GameObject keyedEnemy;
    public float spawnRadius = 10f;

    //spawnInterval - the delay between each enemy spawn, shorter time like 0.1f -> effectively spawns the entire wave simultaneously
    public float spawnInterval = 2f;   
}
