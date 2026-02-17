using UnityEngine;
using UnityEngine.SceneManagement;

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
            
            //Debug.Log("Loaded by the Persist Object from the PersistenceInitializer script");
            //Application.targetFrameRate = 60;
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTOBJECTS")));
            loaded = true;
            
        }
        
    }
}
