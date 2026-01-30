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
    [Tooltip("Total enemies in the wave that INCLUDES the keyed enemy.")]
    public int totalEnemies;
    [Tooltip("The array of all enemy types to spawn in a wave EXCLUDING the KEYED ENEMY.")]
    public GameObject[] enemyTypesArray;
    [Tooltip("The specific enemy that can drop a key and spawns LAST in a wave.")]
    public GameObject keyedEnemy;

    [Tooltip("The amount of each enemy type specified in enemyTypesArray to spawn, MAKE IT EQUAL TO (totalEnemies - 1)")]
    public int[] enemySpawnCounts;
    public float spawnRadius = 10f;

    [Tooltip("Delay between each enemy spawn. Shorter time like 0.1 effectively spawn the entire wave simultaneously.")]
    public float spawnInterval = 2f;   

    public bool spawnCountOrderRandomized = false;
}
