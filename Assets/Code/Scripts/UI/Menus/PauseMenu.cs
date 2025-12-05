using UnityEngine;
using UnityEngine.UI;
using TMPro;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public enum pauseOptions
{
    CONTINUE, SETTINGS, QUIT
}
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private pauseOptions currentPauseOption = pauseOptions.CONTINUE;
    [SerializeField] private Image selectedPauseOption;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private TextMeshProUGUI quitText;
    private Vector2 currentPos;
    private Vector2 originPos;

    void Start()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.enabled = false;
            currentPos = selectedPauseOption.rectTransform.anchoredPosition;
            originPos = currentPos;
            InputTranslator.OnPauseEvent += showPauseMenu;
            InputTranslator.OnResumeEvent += hidePauseMenu;
            InputTranslator.OnPauseUpInput += switchOptionUp;
            InputTranslator.OnPauseDownInput += switchOptionDown;
            InputTranslator.OnSelectEvent += ChooseOption;
        }
        else
        {
            Debug.Log("Pause Menu Canvas is null!");
        }
    }

    void OnDestroy()
    {
        InputTranslator.OnPauseEvent -= showPauseMenu;
        InputTranslator.OnResumeEvent -= hidePauseMenu;
        InputTranslator.OnPauseUpInput -= switchOptionUp;
        InputTranslator.OnPauseDownInput -= switchOptionDown;
        InputTranslator.OnSelectEvent -= ChooseOption;
    }
    private void ChooseOption()
    {
        if(!pauseMenuCanvas.enabled) return;
        switch(currentPauseOption)
        {
            case pauseOptions.CONTINUE:
                hidePauseMenu();
                InputTranslator.Instance.PlayerInputs.Gameplay.Enable();
                InputTranslator.Instance.PlayerInputs.PauseMenu.Disable();
                break;
            case pauseOptions.QUIT:
                Application.Quit();
                break;
            default:
                break;
        }
    }

    private void showPauseMenu()
    {
        pauseMenuCanvas.enabled = true;
        VolumeManager.Instance.SetDepthOfField(true);
        AudioManager.Instance.SetMasterVolume(0.4f);
        Time.timeScale = 0f;
    }

    private void hidePauseMenu()
    {
        pauseMenuCanvas.enabled = false;
        VolumeManager.Instance.SetDepthOfField(false);
        AudioManager.Instance.SetMasterVolume(1f);
        Time.timeScale = 1f;
    }

    private void switchOptionUp()
    {
        if (currentPauseOption == pauseOptions.CONTINUE)
        {
            currentPauseOption = pauseOptions.QUIT;
            selectedPauseOption.rectTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y - 180);
            currentPos = selectedPauseOption.rectTransform.anchoredPosition;
        }
        else if (currentPauseOption == pauseOptions.SETTINGS)
        {
            currentPauseOption = pauseOptions.CONTINUE;
            selectedPauseOption.rectTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y + 90);
            currentPos = selectedPauseOption.rectTransform.anchoredPosition;
        }
        else if (currentPauseOption == pauseOptions.QUIT)
        {
            currentPauseOption = pauseOptions.SETTINGS;
            selectedPauseOption.rectTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y + 90);
            currentPos = selectedPauseOption.rectTransform.anchoredPosition;
        }
    }

    private void switchOptionDown()
    {
        if (currentPauseOption == pauseOptions.CONTINUE)
        {
            currentPauseOption = pauseOptions.SETTINGS;
            selectedPauseOption.rectTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y - 90);
            currentPos = selectedPauseOption.rectTransform.anchoredPosition;

        }
        else if (currentPauseOption == pauseOptions.SETTINGS)
        {
            currentPauseOption = pauseOptions.QUIT;
            selectedPauseOption.rectTransform.anchoredPosition = new Vector2(currentPos.x, currentPos.y - 90);
            currentPos = selectedPauseOption.rectTransform.anchoredPosition;

        }
        else if (currentPauseOption == pauseOptions.QUIT)
        {
            currentPauseOption = pauseOptions.CONTINUE;
            selectedPauseOption.rectTransform.anchoredPosition = new Vector2(originPos.x, originPos.y);
            currentPos = selectedPauseOption.rectTransform.anchoredPosition;
        }
    }
}
