using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;

[System.Serializable]
public class StringGameobjectPair
{
    public string key;
    public GameObject value;
}

public class MenuManager : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
    public static MenuManager Instance { get; private set; }
    [SerializeField] private List<StringGameobjectPair> menuDictionary;

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
        //PlayerStats.OnPlayerDeathEvent += EnableGameoverMenu;
        //_translator.OnPauseEvent += EnablePauseMenu;
        //_translator.OnResumeEvent += DisablePauseMenu;
    }

    void OnDestroy()
    {
        //PlayerStats.OnPlayerDeathEvent -= EnableGameoverMenu;
        //_translator.OnPauseEvent -= EnablePauseMenu;
        //_translator.OnResumeEvent -= DisablePauseMenu;
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
        VolumeManager.Instance.SetDepthOfField(true);
        AudioManager.Instance.SetMasterVolume(0.4f);
        Time.timeScale = 0f;
    }

    private void DisablePauseMenu()
    {
        SetMenu("HUD");
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
}
