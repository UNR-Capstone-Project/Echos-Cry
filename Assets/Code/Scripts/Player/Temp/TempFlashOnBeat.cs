using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class TempFlashOnBeat : MonoBehaviour
{
    Light light = null;
    float tempHold = 0;

    private void Awake()
    {
        light = GetComponent<Light>();
    }
    void Start()
    {
        TempoManager.BeatTickEvent += FlashLight;
    }
    private void OnDestroy()
    {
        TempoManager.BeatTickEvent -= FlashLight;
    }

    private void Update()
    {
        tempHold += Time.deltaTime;
        light.intensity = (tempHold / TempoManager.TimeBetweenBeats) * 3.5f;
    }

    private void FlashLight()
    {
        StartCoroutine(FlashLightCoroutine());
    }
    private IEnumerator FlashLightCoroutine()
    {
        yield return new WaitForSeconds(TempoManager.TimeBetweenBeats);
        light.intensity = 0;
        tempHold = 0;
    }

}
