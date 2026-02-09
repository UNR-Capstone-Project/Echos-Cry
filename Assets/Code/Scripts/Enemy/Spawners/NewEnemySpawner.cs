using System;
using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public class NewEnemySpawner : MonoBehaviour
{
    public GameObject spawnDecal;
    private float spawnFromDecalWait = 3f;

    public IEnumerator SpawnWithDecal(EnemyPool enemyPool, Vector3 initialPos, float samplingDistance, Action<Enemy> callback)
    {
        NavMeshHit hit;
        Vector3 spawnPos = new Vector3(1f,1f,1f);
        if (NavMesh.SamplePosition(initialPos, out hit, samplingDistance, NavMesh.AllAreas)) {
            spawnPos = hit.position;
        }
        var decal = Instantiate(spawnDecal, spawnPos, Quaternion.Euler(90f, 0f, 0f));

        yield return new WaitForSeconds(spawnFromDecalWait);
        Enemy instance = enemyPool.GetEnemy();

        //to get it to spawn where i want to instead of it teleporting to the location of the pool manager gameobject
        if (instance.TryGetComponent(out NavMeshAgent agent)) agent.enabled = false;
        instance.transform.position = spawnPos;
        if (agent != null) agent.enabled = true;

        callback?.Invoke(instance);
    }

    public Vector3 GetRandomPoint(float radius)
    {
        return transform.position + UnityEngine.Random.insideUnitSphere * radius;
    }

}
