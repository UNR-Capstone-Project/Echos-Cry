using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class TempFlashOnBeat : MonoBehaviour
{
    Light lightIntensity = null;
    float tempHold = 0;

    private void Awake()
    {
        lightIntensity = GetComponent<Light>();
    }
    void Start()
    {
        TempoManager.BeatTickEvent += FlashLight;
        lightIntensity.intensity = 0;
    }
    private void OnDestroy()
    {
        TempoManager.BeatTickEvent -= FlashLight;
    }

    private void Update()
    {
        tempHold += Time.deltaTime;
        lightIntensity.intensity = (tempHold / TempoManager.TimeBetweenBeats) * 3.5f;
    }

    private void FlashLight()
    {
        StartCoroutine(FlashLightCoroutine());
    }
    private IEnumerator FlashLightCoroutine()
    {
        yield return new WaitForSeconds(TempoManager.TimeBetweenBeats);
        lightIntensity.intensity = 0;
        tempHold = 0;
    }

}
