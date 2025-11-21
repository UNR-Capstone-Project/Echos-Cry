using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;

public class NewEnemySpawner : MonoBehaviour
{
    public GameObject spawnDecal;
    private float decalLifetime = 3f;

    public IEnumerator SpawnWithDecal(GameObject prefab, Vector3 initialPos, float samplingDistance)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(initialPos, out hit, samplingDistance, NavMesh.AllAreas)) initialPos = hit.position;

        var decal = Instantiate(spawnDecal, initialPos, Quaternion.Euler(90f, 0f, 0f));
        Destroy(decal, decalLifetime);
        yield return new WaitForSeconds(decalLifetime);
        Instantiate(prefab, initialPos, Quaternion.identity);
    }

    public Vector3 GetRandomPoint(float radius)
    {
        return transform.position + UnityEngine.Random.insideUnitSphere * radius;
    }

    

    /* 
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
        GameObject decalInstance = Instantiate(EnemyPrefabs[0], spawnPosition, Quaternion.identity);
    }
    */
}
