using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[System.Serializable]
public class StringGameobjectPair
{
    public string key;
    public GameObject value;
}

public class MenuManager : Singleton<MenuManager>
{
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private GameObject screenFadeObject;
    [SerializeField] private List<StringGameobjectPair> menuDictionary;
    [SerializeField] private InputTranslator _inputTranslator;
    [SerializeField] private SettingsManager _settingsManager;

    public static event Action PauseStarted;
    public static event Action PauseEnded;

    private string _currentMenu;

    protected override void OnAwake()
    {
        SetMenu("HUD");
    }

    private void Start()
    {
        GameManager.OnPlayerDeathEvent += EnableGameoverMenu;
        DialogueEvents.Instance.OnDialogueStarted += () => SetMenu("Dialogue");
        DialogueEvents.Instance.OnDialogueEnded += () => SetMenu("HUD");
        _settingsManager.OnMenuBackButton += () => SetMenu("Pause");
        _translator.OnUpgradeEvent += EnableUpgradeMenu;
        _translator.OnPauseEvent += EnablePauseMenu;
        _translator.OnResumeEvent += DisablePauseMenu;
    }

    void OnDestroy()
    {
        GameManager.OnPlayerDeathEvent -= EnableGameoverMenu;
        DialogueEvents.Instance.OnDialogueStarted -= () => SetMenu("Dialogue");
        DialogueEvents.Instance.OnDialogueEnded -= () => SetMenu("HUD");
        _settingsManager.OnMenuBackButton -= () => SetMenu("Pause");
        _translator.OnUpgradeEvent -= EnableUpgradeMenu;
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
        _inputTranslator.PlayerInputs.PauseMenu.Enable();
        _inputTranslator.PlayerInputs.Gameplay.Disable();

        SetMenu("Pause");
        PauseStarted?.Invoke();
        VolumeManager.Instance.SetDepthOfField(true);
        Time.timeScale = 0f;
    }

    private void EnableUpgradeMenu()
    {
        _inputTranslator.PlayerInputs.PauseMenu.Enable();
        _inputTranslator.PlayerInputs.Gameplay.Disable();

        SetMenu("Upgrade");
        PauseStarted?.Invoke();
        VolumeManager.Instance.SetDepthOfField(true);
        Time.timeScale = 0f;
    }

    public void DisablePauseMenu()
    {
        _inputTranslator.PlayerInputs.Gameplay.Enable();
        _inputTranslator.PlayerInputs.PauseMenu.Disable();
        
        SetMenu("HUD");
        PauseEnded?.Invoke();
        VolumeManager.Instance.SetDepthOfField(false);
        Time.timeScale = 1f;
    }

    public void SetMenu(string menuName)
    {
        _currentMenu = menuName;
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

    public void BackButton()
    {
        MenuManager.Instance.DisablePauseMenu();
    }
}