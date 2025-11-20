using UnityEngine;

[CreateAssetMenu(fileName = "WaveData", menuName = "Scriptable Objects/WaveData")]
public class WaveData : ScriptableObject
{
    public GameObject[] enemiesArray;
    public GameObject keyedEnemy;
    public float spawnRadius = 10f;
    public float spawnInterval = 2f;
}
