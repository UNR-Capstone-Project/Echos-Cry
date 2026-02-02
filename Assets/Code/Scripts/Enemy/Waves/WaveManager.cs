using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor

public class WaveManager : MonoBehaviour
{
    public event Action OnNewWaveSpawned, OnWaveSpawningEnded, OnAllWavesCompleted;

    [SerializeField] private float _timeBetweenWaves = 10f;
    [SerializeField] private WaveData[] _allWaves;
    [SerializeField] private NewEnemySpawner _enemySpawner;

    private int _currentWave = 0;
    private int _totalEnemiesKilled = 0;

    private int GetTotalEnemiesInWave(WaveData currentWave)
    {
        int count = 0;
        foreach (SpawnData enemySpawns in currentWave.EnemySpawns)
        {
            count += enemySpawns.enemySpawnCount;
        }
        return count;
    }
    public void UpdateKillCount()
    {
        _totalEnemiesKilled++;

        if (_totalEnemiesKilled >= GetTotalEnemiesInWave(_allWaves[_currentWave]))
        {
            _currentWave++;

            if (_currentWave >= _allWaves.Length) //All waves completed
            {
                HUDMessage.Instance.UpdateMessage("Waves Completed!", 2f);
                OnAllWavesCompleted?.Invoke();
            }
            else
            {
                StartCoroutine(SpawnWaveAfterDelay(_timeBetweenWaves));
            }
        }
    }
    private IEnumerator SpawnWaveAfterDelay(float seconds)
    {
        HUDMessage.Instance.UpdateMessage("New Wave In " + (seconds).ToString() + " Seconds.", 1.5f);
        yield return new WaitForSeconds(seconds);
        StartNextWave();
    }
    public void StartNextWave()
    {
        _totalEnemiesKilled = 0;

        HUDMessage.Instance.UpdateMessage("Wave " + (_currentWave + 1).ToString() + " Has Begun.", 2f);
        CameraManager.Instance.ScreenShake(0.4f, 2.5f);
        StartCoroutine(SpawnWave(_allWaves[_currentWave]));
    }
    private IEnumerator SpawnWave(WaveData wave)
    {
        OnNewWaveSpawned?.Invoke();

        for (int i = 0; i < wave.EnemySpawns.Length; i++)
        {
            for (int j = 0; j < wave.EnemySpawns[i].enemySpawnCount; j++)
            {
                GameObject enemyPrefab = wave.EnemySpawns[i].enemySpawnType;
                Vector3 enemyPosition = _enemySpawner.GetRandomPoint(wave.spawnRadius);
                StartCoroutine(_enemySpawner.SpawnWithDecal(enemyPrefab, enemyPosition, wave.spawnRadius, (enemyInstance) =>
                {
                    enemyInstance.transform.SetParent(_enemySpawner.transform);
                    HealthSystem stats = enemyInstance.GetComponent<HealthSystem>();
                }));
            }
            yield return new WaitForSeconds(wave.spawnInterval);
        }

        OnWaveSpawningEnded?.Invoke();
        yield return null;
    }
}