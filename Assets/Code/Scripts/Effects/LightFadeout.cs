using System.Collections;
using UnityEngine;

public class LightFadeout : MonoBehaviour
{
    [SerializeField] float delayTime;
    [SerializeField] float fadeRate;
    Light _light;
    bool _delayed = true;
    private void Awake()
    {
        _light = GetComponent<Light>();
    }
    private void Start()
    {
        StartCoroutine(DelayTimeCoroutine());
    }
    private IEnumerator DelayTimeCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        _delayed = false;
    }

    private void Update()
    {
        if (!_delayed && _light != null)
        {
            _light.intensity -= fadeRate * Time.deltaTime;
        }
    }
}
