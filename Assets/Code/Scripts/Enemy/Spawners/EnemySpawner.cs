using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawner : MonoBehaviour
{
    public float spawnRadius = 10f;
    public float spawnWait = 10f;
    private Terrain terrain;
    public GameObject spawnDecal;
    public GameObject[] EnemyPrefabs;

    private float spawnTimer;
    private float decalDestroyTime = 3f;

    void Start()
    {
        terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.Log("Main terrain was not assigned or found!");
        }
        else
        {
            spawnTimer = spawnWait;
        }
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            terrain = Terrain.activeTerrain; //ISSUE: Not ideal here, but terrain is getting lost on scene change!
            //There is a on scene change method that could be used to fix above mentioned issue!
            spawnEnemyDecal();
            spawnTimer = spawnWait;
        }
    }

    private void spawnEnemyDecal()
    {
        Vector3 randomOffset = UnityEngine.Random.insideUnitCircle * spawnRadius;
        Vector3 randomWorldPos = transform.position + new Vector3(randomOffset.x, 0f, randomOffset.y);

        float terrainHeight = terrain.SampleHeight(randomWorldPos);
        Vector3 spawnPoint = new Vector3(randomWorldPos.x, terrainHeight + 1f, randomWorldPos.z);
        Vector3 decalPoint = new Vector3(randomWorldPos.x, terrainHeight + 0.1f, randomWorldPos.z);

        GameObject decalInstance = Instantiate(spawnDecal, decalPoint, Quaternion.Euler(90f, 0f, 0f));
        decalInstance.GetComponent<timedDestroy>().setDestroy(decalDestroyTime);
        StartCoroutine(spawnEnemy(spawnPoint));
    }

    IEnumerator spawnEnemy(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(decalDestroyTime);
        int randomIndex = UnityEngine.Random.Range(0, EnemyPrefabs.Length);
        GameObject decalInstance = Instantiate(EnemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);
    }
}
