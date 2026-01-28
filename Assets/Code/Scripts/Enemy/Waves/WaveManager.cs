using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
/// 

//ISSUE: Cannot spawn more than one enemy at once!
public class WaveManager : MonoBehaviour
{
    public event Action OnNewWaveSpawned, OnWaveSpawningEnded, OnAllWavesCompleted;

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

    void OnEnable()
    {
        //Enemy.enemyKilled += updateKillCount;
    }

    void OnDisable()
    {
        //Enemy.enemyKilled -= updateKillCount;
    }

    public void updateKillCount()
    {
        Debug.Log($"total killed so far: {totalEnemiesKilled}");
        totalEnemiesKilled++;

        if (totalEnemiesKilled >= allWaves[currentWave].totalEnemies)
        {
            Debug.Log($"Total enemies killed match the current wave {currentWave+1}, all waves completed.");
            currentWave++;
            if (currentWave >= allWaves.Length) //All waves completed
            {
                HUDMessage.Instance.UpdateMessage("Waves Completed!", 2f);
                OnAllWavesCompleted?.Invoke();
                return;
            }
            else //Start next wave
            {
                Debug.Log($"Total enemies killed match the current wave {currentWave+1}, moving to next wave");
                StartCoroutine(spawnWaveAfterDelay(timeBetweenWaves));
            }
        }
    }

    public void startNewWave()
    {
        totalEnemiesKilled = 0;
        if (currentWave >= allWaves.Length) return;

        HUDMessage.Instance.UpdateMessage("Wave " + (currentWave + 1).ToString() + " Has Begun.", 2f);
        CameraManager.Instance.ScreenShake(0.4f, 2.5f);
        StartCoroutine(spawnWave(allWaves[currentWave]));
    }

    private GameObject getRandomEnemy(WaveData inputWave)
    { //ISSUE: This can potentially select the same enemy type multiple times in a wave. Fix later if needed.
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
        OnNewWaveSpawned?.Invoke();
        for (int i = 0; i < wave.totalEnemies - 1; i++)
        {
            GameObject enemy = getRandomEnemy(wave);
            Vector3 enemyPosition = enemySpawner.GetRandomPoint(wave.spawnRadius);

            StartCoroutine(enemySpawner.SpawnWithDecal(enemy, enemyPosition, wave.spawnRadius, (enemyInstance) =>
            {
                enemyInstance.transform.SetParent(enemySpawner.transform);
                EnemyStats stats = enemyInstance.GetComponent<EnemyStats>();
            }));

            yield return new WaitForSeconds(wave.spawnInterval);
        }

        GameObject keyedEnemy = wave.keyedEnemy;
        Vector3 keyedEnemyPosition = enemySpawner.GetRandomPoint(wave.spawnRadius);
        GameObject keyedInstance = Instantiate(keyedEnemy, keyedEnemyPosition, Quaternion.identity, enemySpawner.transform);
        var keyedStats = keyedInstance.GetComponent<EnemyStats>();

        OnWaveSpawningEnded?.Invoke();
        yield return null;
    }
}
