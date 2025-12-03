using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
/// 
/// <summary>
/// A ScriptableObject to hold all the data about enemy spawn waves to be used in an area of a level
/// </summary>
[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public int totalEnemies;
    public GameObject[] enemyTypesArray;
    public GameObject keyedEnemy;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;
}
