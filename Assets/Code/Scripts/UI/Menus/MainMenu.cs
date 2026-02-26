using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private List<StringGameobjectPair> menuDictionary;
    [SerializeField] private SettingsManager settingsManager;

    public void SetMenu(string menuName)
    {
        foreach (StringGameobjectPair menu in menuDictionary)
        {
            if (menu.key == menuName) { menu.value.SetActive(true); }
            else { menu.value.SetActive(false); }
        }
    }

    private void OnAwake()
    {
        SetMenu("Main");
    }
    private void OnEnable()
    {
        settingsManager.OnMenuBackButton += HandleBackButton;
    }
    private void OnDisable()
    {
        settingsManager.OnMenuBackButton += HandleBackButton;
    }
    private void HandleBackButton()
    {
        SetMenu("Main");
    }

    public void Play()
    {
        SceneManager.LoadScene("TownScene");
    }

    public void Settings()
    {
        SetMenu("Settings");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
