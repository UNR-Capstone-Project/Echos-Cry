using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
/// 
/// <summary>
/// A ScriptableObject to hold all the data about enemy spawn waves to be used in an area of a level
/// </summary>

[System.Serializable]
public struct SpawnData
{
    public GameObject enemySpawnType;
    public int enemySpawnCount;
}

[CreateAssetMenu(fileName = "WaveData", menuName = "Echo's Cry/Enemy Wave System/WaveData")]
public class WaveData : ScriptableObject
{
    [Tooltip("List of every enemy type and how many will spawn in a given wave.")] 
    public SpawnData[] EnemySpawns;

    public float spawnRadius = 10f;
    public float spawnInterval; 
}
