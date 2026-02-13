using UnityEngine;
using UnityEngine.SceneManagement;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public class MainMenu : MonoBehaviour
{
    public void Continue()
    {
        SceneManager.LoadScene("TownScene");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("TownScene");
    }

    public void Settings()
    {
        Debug.Log("Opening settings!");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
