using UnityEngine;
using UnityEngine.SceneManagement;
/// Original Author: Andy
/// All Contributors Since Creation: Victor, Andy
/// Last Modified By: Victor
public static class PersistenceInitializer
{
    private static bool loaded = false;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        SceneManager.sceneLoaded += LoadObjects;
    }

    static void LoadObjects(Scene scene, LoadSceneMode mode)
    {
        if (loaded) return;

        var currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "MainMenu")
        {
            //Code Section Begins. Code Author: Andy
            Debug.Log("Loaded by the Persist Object from the PersistenceInitializer script");
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTOBJECTS")));
            loaded = true;
            //Code Section Ends.
        }
        
    }
}
