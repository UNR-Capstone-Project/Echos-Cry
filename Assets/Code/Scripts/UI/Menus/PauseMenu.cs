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
    [SerializeField] private InputTranslator translator;
    [SerializeField] private pauseOptions currentPauseOption = pauseOptions.CONTINUE;
    [SerializeField] private Image selectedPauseOption;
    [SerializeField] private TextMeshProUGUI continueText;
    [SerializeField] private TextMeshProUGUI settingsText;
    [SerializeField] private TextMeshProUGUI quitText;
    private Vector2 currentPos;
    private Vector2 originPos;

    void Start()
    {
        currentPos = selectedPauseOption.rectTransform.anchoredPosition;
        originPos = currentPos;

        translator.OnPauseUpInput += switchOptionUp;
        translator.OnPauseDownInput += switchOptionDown;
        translator.OnSelectEvent += ChooseOption;
    }

    void OnDestroy()
    {
        translator.OnPauseUpInput -= switchOptionUp;
        translator.OnPauseDownInput -= switchOptionDown;
        translator.OnSelectEvent -= ChooseOption;
    }
    private void ChooseOption()
    {
        switch(currentPauseOption)
        {
            case pauseOptions.CONTINUE:
                MenuManager.Instance.SetMenu("HUD");
                translator.PlayerInputs.Gameplay.Enable();
                translator.PlayerInputs.PauseMenu.Disable();
                break;
            case pauseOptions.QUIT:
                Application.Quit();
                break;
            default:
                break;
        }
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
