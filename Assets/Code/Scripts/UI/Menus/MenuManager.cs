using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Collections;
using System;

[System.Serializable]
public class StringGameobjectPair
{
    public string key;
    public GameObject value;
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private GameObject screenFadeObject;
    [SerializeField] private List<StringGameobjectPair> menuDictionary;
    public static MenuManager Instance { get; private set; }
    public static event Action PauseStarted;
    public static event Action PauseEnded;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        SetMenu("HUD");
    }

    private void Start()
    {
        PlayerStats.OnPlayerDeathEvent += EnableGameoverMenu;
        _translator.OnPauseEvent += EnablePauseMenu;
        _translator.OnResumeEvent += DisablePauseMenu;
    }

    void OnDestroy()
    {
        PlayerStats.OnPlayerDeathEvent -= EnableGameoverMenu;
        _translator.OnPauseEvent -= EnablePauseMenu;
        _translator.OnResumeEvent -= DisablePauseMenu;
    }

    public void EnableGameoverMenu()
    {
        SetMenu("Gameover");
        VolumeManager.Instance.SetDepthOfField(true);
        VolumeManager.Instance.SetColorSaturationGrey();
        Time.timeScale = 0f;
    }

    private void EnablePauseMenu()
    {
        SetMenu("Pause");
        PauseStarted?.Invoke();
        VolumeManager.Instance.SetDepthOfField(true);
        AudioManager.Instance.SetMasterVolume(0.4f);
        Time.timeScale = 0f;
    }

    public void DisablePauseMenu()
    {
        SetMenu("HUD");
        PauseEnded?.Invoke();
        VolumeManager.Instance.SetDepthOfField(false);
        AudioManager.Instance.SetMasterVolume(1f);
        Time.timeScale = 1f;
    }

    public void SetMenu(string menuName)
    {
        foreach (StringGameobjectPair menu in menuDictionary) 
        { 
            if (menu.key == menuName) 
            {
                menu.value.SetActive(true);
            }
            else
            {
                menu.value.SetActive(false);
            }
        }
    }

    public void ScreenFadeIn()
    {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine()
    {
        float duration = 2f;
        float elapsedTime = 0f;

        CanvasGroup canvasGroup = screenFadeObject.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        yield return new WaitForSeconds(1f);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            canvasGroup.alpha = 1f - Mathf.Clamp01(elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
