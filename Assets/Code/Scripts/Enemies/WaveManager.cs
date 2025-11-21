using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private int currentWave;
    public WaveData[] allWaves;
    public event Action newWaveSpawned, AWaveEndedSpawning, allWavesCompleted;
    public NewEnemySpawner spawner;
    private int totalEnemiesKilled;

    private void Awake()
    {
        totalEnemiesKilled = 0;
        currentWave = 0;
    }

    public void updateKillCount()
    {
        
    }

    public void startNewWave()
    {
        if (currentWave > allWaves.Length)
        {
            allWavesCompleted?.Invoke();
            return;
        }

        if (allWaves.Length == 1)
        {
            StartCoroutine(spawnWave(allWaves[currentWave]));
        } else
        {
            if (currentWave >= 0 && currentWave <= allWaves.Length)
            {
                StartCoroutine(spawnWave(allWaves[currentWave]));
            }
            currentWave++;
        }

        

    }

    private GameObject getRandomEnemy(WaveData inputWave)
    {
        int index;
        if (inputWave.enemyTypesArray.Length > 0)
        {
            index = UnityEngine.Random.Range(0, inputWave.enemyTypesArray.Length);
        } else
        {
            index = 0;
        }
        return inputWave.enemyTypesArray[index];
    }

    private IEnumerator spawnWave(WaveData wave)
    {
        int index;
        newWaveSpawned?.Invoke();
        for (int i = 0; i < wave.totalEnemies-1; i++)
        {
            if (wave.enemyTypesArray.Length > 1)
            {
                index = UnityEngine.Random.Range(0, wave.enemyTypesArray.Length);
            } else
            {
                index = 0;
            }
            GameObject enemy = getRandomEnemy(wave);
            Vector3 enemyPosition = spawner.GetRandomPoint(wave.spawnRadius);
            StartCoroutine(spawner.SpawnWithDecal(enemy, enemyPosition, wave.spawnRadius));
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        GameObject keyedEnemy = wave.keyedEnemy;
        Vector3 keyedEnemyPosition = spawner.GetRandomPoint(wave.spawnRadius);
        GameObject keyedInstance = Instantiate(keyedEnemy, keyedEnemyPosition, Quaternion.identity);

        AWaveEndedSpawning?.Invoke();
        yield return null;
    }

}
