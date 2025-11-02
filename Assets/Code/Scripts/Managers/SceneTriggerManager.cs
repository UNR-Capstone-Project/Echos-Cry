using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneTriggerManager is for loading scenes with a clear distinction between each other
/// eg. loading from a level to a completely different level, or boss arena, etc 
/// </summary>
public class SceneTriggerManager : MonoBehaviour
{
    [SerializeField] private SceneField sceneTarget;
    [SerializeField] private SceneField sceneOrigin;

    private bool sceneTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !sceneTransitioning)
        {
            StartCoroutine(HandleSceneTransition());
        }
    }

    IEnumerator HandleSceneTransition()
    {
        sceneTransitioning = true;
        AsyncOperation newSceneLoad = SceneManager.LoadSceneAsync(sceneTarget.SceneName, LoadSceneMode.Additive);
        newSceneLoad.allowSceneActivation = true; //When set to true, the player is able to see during scene loading.

        while (!newSceneLoad.isDone) { yield return null; }

        AsyncOperation oldSceneUnload = SceneManager.UnloadSceneAsync(sceneOrigin.SceneName);
        while (oldSceneUnload != null && !oldSceneUnload.isDone)
        {
            yield return null;
        }

        sceneTransitioning = false;
    }
}
