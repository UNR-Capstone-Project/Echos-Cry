using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using System;

public class TempFlashOnBeat : MonoBehaviour
{
    private void SetGameStartBool()
    {
        gameStarted = true;
    }
    private void EnableLight()
    {
        if (!gameStarted) return;
        gameObject.SetActive(true);
    }
    private void DisableLight()
    {
        gameObject.SetActive(false);
    }

    private void FlashLight()
    {
        if (!gameObject.activeSelf) return;
        StartCoroutine(FlashLightCoroutine());
    }
    private IEnumerator FlashLightCoroutine()
    {
        yield return new WaitForSeconds(TempoManager.TimeBetweenBeats);
        lightIntensity.intensity = 0;
        tempHold = 0;
    }

    private void Awake()
    {
        lightIntensity = GetComponent<Light>();
    }
    void Start()
    {
        MusicManager.Instance.SongPlayEvent += EnableLight;
        MusicManager.Instance.SongStopEvent += DisableLight;

        TempoManager.BeatTickEvent += FlashLight;

        lightIntensity.intensity = 0;

        gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        MusicManager.Instance.SongPlayEvent -= EnableLight;
        MusicManager.Instance.SongStopEvent -= DisableLight;
        TempoManager.BeatTickEvent -= FlashLight;
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;
        tempHold += Time.deltaTime;
        lightIntensity.intensity = (tempHold / TempoManager.TimeBetweenBeats) * 3.5f;
    }

    Light lightIntensity = null;
    float tempHold = 0;
    bool gameStarted = false;
}
