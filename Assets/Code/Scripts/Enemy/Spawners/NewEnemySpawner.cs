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

    public IEnumerator SpawnWithDecal(GameObject prefab, Vector3 initialPos, float samplingDistance, Action<GameObject> callback)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(initialPos, out hit, samplingDistance, NavMesh.AllAreas)) initialPos = hit.position;
        var decal = Instantiate(spawnDecal, initialPos, Quaternion.Euler(90f, 0f, 0f));

        yield return new WaitForSeconds(spawnFromDecalWait);
        GameObject instance = Instantiate(prefab, initialPos, Quaternion.identity);

        callback?.Invoke(instance);
    }

    public Vector3 GetRandomPoint(float radius)
    {
        return transform.position + UnityEngine.Random.insideUnitSphere * radius;
    }

}
