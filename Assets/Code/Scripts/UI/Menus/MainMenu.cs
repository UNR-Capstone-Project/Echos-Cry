using UnityEngine;
using UnityEngine.SceneManagement;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public class MainMenu : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene("TownScene");
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
