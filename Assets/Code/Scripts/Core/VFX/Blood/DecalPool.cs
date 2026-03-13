using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Rendering.Universal;

public class DecalPool : MonoBehaviour
{
    [SerializeField] float timeBeforeFade = 10f;
    [SerializeField] float fadeMultiplier = 3f;
    [SerializeField] GameObject _prefab;
    //bool[] _prefabInPool; 
    ObjectPool<DecalProjector> _decalPool;

    public DecalProjector GetDecal()
    {
        return _decalPool.Get();
    }
    public void ReleaseDecal(DecalProjector decal)
    {
        _decalPool.Release(decal);
    }
    private IEnumerator FadeAfterTime(DecalProjector decal)
    {
        yield return new WaitForSeconds(timeBeforeFade);
        StartCoroutine(BeginFade(decal));
    }
    private IEnumerator BeginFade(DecalProjector decal)
    {
        while (decal.fadeFactor > 0)
        {
            decal.fadeFactor -= Time.deltaTime * fadeMultiplier;
            yield return null;
        }
        ReleaseDecal(decal);
    }

    private void Awake()
    {
        _decalPool = new ObjectPool<DecalProjector>
            (
                createFunc: OnCreateDecal,
                actionOnGet: OnGetDecal,
                actionOnRelease: OnReleaseDecal,
                actionOnDestroy: OnDestroyDecal,
                true,
                10,
                100
            );

        //_prefabInPool = new bool[_prefab.Length];
        //for(int i = 0; i < _prefabInPool.Length; i++) _prefabInPool[i] = false;
    }
    private void Start()
    {
        //TESTING: REMOVE AFTER
        DecalProjector decal = _decalPool.Get();
        decal.transform.position = PlayerRef.Transform.position;
    }
    private DecalProjector OnCreateDecal()
    {
        GameObject newObject = Instantiate(_prefab, Vector3.zero, Quaternion.identity * Quaternion.AngleAxis(90f, Vector3.right), transform);
        return newObject.GetComponent<DecalProjector>();
    }
    private void OnGetDecal(DecalProjector decalProjector)
    {
        decalProjector.gameObject.SetActive(true);
        StartCoroutine(FadeAfterTime(decalProjector));
    }
    private void OnReleaseDecal(DecalProjector decalProjector)
    {
        decalProjector.fadeFactor = 1f;
        decalProjector.gameObject.SetActive(false);
    }
    private void OnDestroyDecal(DecalProjector decalProjector)
    {
        
    }
}