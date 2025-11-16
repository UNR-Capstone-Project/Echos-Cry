using UnityEngine;
using UnityEngine.SceneManagement;

public static class PersistenceInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]

    public static void Execute()
    {
        var currentScene = SceneManager.GetActiveScene();

        if (currentScene.name != "MainMenu")
        {
            Debug.Log("Loaded by the Persist Object from the PersistenceInitializer script");
            Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("PERSISTOBJECTS")));
        }
        
    }
}
