using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum pauseOptions
{
    CONTINUE, SETTINGS, QUIT
}
public class PauseMenu : MonoBehaviour
{
    private pauseOptions currentPauseOption = pauseOptions.CONTINUE;
    
    [SerializeField] private InputTranslator translator;

    private void ChooseOption()
    {
        switch(currentPauseOption)
        {
            case pauseOptions.CONTINUE:
                MenuManager.Instance.SetMenu("HUD");
                MenuManager.Instance.DisablePauseMenu();
                translator.PlayerInputs.Gameplay.Enable();
                translator.PlayerInputs.PauseMenu.Disable();
                break;
            case pauseOptions.SETTINGS:
                MenuManager.Instance.SetMenu("Settings");
                break;
            case pauseOptions.QUIT:
                Application.Quit();
                break;
            case pauseOptions.SETTINGS:
                HandleSettingsMenu();
                break;
            default:
                break;
        }
    }

    public void SelectPauseOption(int option)
    {
        currentPauseOption = (pauseOptions)option;
        ChooseOption();
    }

    public void HandleSettingsMenu()
    {
        
    }
}
