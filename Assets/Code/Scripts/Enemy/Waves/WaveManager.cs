using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public class WaveManager : MonoBehaviour
{
    public event Action newWaveSpawned, AWaveEndedSpawning; //allWavesCompleted;

    [SerializeField] private NewEnemySpawner enemySpawner;
    [SerializeField] private float timeBetweenWaves = 10f;
    [SerializeField] private WaveData[] allWaves;

    private int currentWave;
    private int totalEnemiesKilled;

    private void Awake()
    {
        totalEnemiesKilled = 0;
        currentWave = 0;
    }

    public void updateKillCount()
    {
        totalEnemiesKilled++;

        if (totalEnemiesKilled >= allWaves[currentWave].totalEnemies)
        {
            currentWave++;
            if (currentWave >= allWaves.Length) return;
            StartCoroutine(spawnWaveAfterDelay(timeBetweenWaves));
        }
    }

    public void startNewWave()
    {
        totalEnemiesKilled = 0;
        if (currentWave >= allWaves.Length) return;

        HUDMessage.Instance.UpdateMessage("Wave " + (currentWave + 1).ToString() + " Has Begun.", 2f);
        StartCoroutine(spawnWave(allWaves[currentWave]));
    }

    private GameObject getRandomEnemy(WaveData inputWave)
    {
        int index;
        if (inputWave.enemyTypesArray.Length > 1)
        {
            index = UnityEngine.Random.Range(0, inputWave.enemyTypesArray.Length);
        } else
        {
            index = 0;
        }
        return inputWave.enemyTypesArray[index];
    }

    private IEnumerator spawnWaveAfterDelay(float seconds)
    {
        HUDMessage.Instance.UpdateMessage("New Wave In " + (seconds).ToString() + " Seconds.", 1.5f);
        yield return new WaitForSeconds(seconds);
        startNewWave();
    }

    private IEnumerator spawnWave(WaveData wave)
    {
        newWaveSpawned?.Invoke();
        for (int i = 0; i < wave.totalEnemies - 1; i++)
        {
            
            GameObject enemy = getRandomEnemy(wave);
            Vector3 enemyPosition = enemySpawner.GetRandomPoint(wave.spawnRadius);
            StartCoroutine(enemySpawner.SpawnWithDecal(enemy, enemyPosition, wave.spawnRadius, (enemyInstance) =>
            {
                enemyInstance.transform.SetParent(enemySpawner.transform);
                EnemyStats stats = enemyInstance.GetComponent<EnemyStats>();
                if (stats != null) stats.OnEnemyDeathEvent += updateKillCount;
            }));
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        GameObject keyedEnemy = wave.keyedEnemy;
        Vector3 keyedEnemyPosition = enemySpawner.GetRandomPoint(wave.spawnRadius);
        GameObject keyedInstance = Instantiate(keyedEnemy, keyedEnemyPosition, Quaternion.identity, enemySpawner.transform);
        var keyedStats = keyedInstance.GetComponent<EnemyStats>();
        if (keyedStats != null) keyedStats.OnEnemyDeathEvent += updateKillCount;

        AWaveEndedSpawning?.Invoke();
        yield return null;
    }

}
