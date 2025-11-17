using AudioSystem;
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
    [SerializeField] soundEffect portalSFX;

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
        //Scene Transition first 
        sceneTransitioning = true;

        AsyncOperation newSceneLoad = SceneManager.LoadSceneAsync(sceneTarget.SceneName, LoadSceneMode.Single);
        newSceneLoad.allowSceneActivation = true;

        while (!newSceneLoad.isDone) { yield return null;  }
        
        sceneTransitioning = false;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        soundEffectManager.Instance.createSound()
            .setSound(portalSFX)
            .setSoundPosition(this.transform.position)
            .ValidateAndPlaySound();
    }
}