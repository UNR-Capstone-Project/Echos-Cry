using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuCanvas;

    void Start()
    {
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.enabled = false;
            InputTranslator.OnPauseEvent += showPauseMenu;
            InputTranslator.OnResumeEvent += hidePauseMenu;
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
    }

    private void showPauseMenu()
    {
        pauseMenuCanvas.enabled = true;
    }

    private void hidePauseMenu()
    {
        pauseMenuCanvas.enabled = false;
    }
    
}
