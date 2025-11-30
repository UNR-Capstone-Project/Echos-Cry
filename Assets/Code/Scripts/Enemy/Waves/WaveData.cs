using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public int totalEnemies;
    public GameObject[] enemyTypesArray;
    public GameObject keyedEnemy;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;
}
