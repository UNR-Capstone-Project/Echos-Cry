using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("LevelOneScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EnterOptions()
    {
        
    }

    public void EnterSettings()
    {
        
    }
}
