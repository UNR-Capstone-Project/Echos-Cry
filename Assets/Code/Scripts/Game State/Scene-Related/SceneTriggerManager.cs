using AudioSystem;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
/// Original Author: Victor
/// All Contributors Since Creation: Victor, Andy
/// Last Modified By: Andy
/// 
/// <summary>
/// SceneTriggerManager is for loading scenes with a clear distinction between each other
/// eg. loading from a level to a completely different level, or boss arena, etc 
/// </summary>
public class SceneTriggerManager : MonoBehaviour
{
    [SerializeField] private SceneField sceneTarget;
    [SerializeField] soundEffect portalSFX;
    [SerializeField] private bool _isLevelExit;
    [SerializeField] private bool _isFinalExit = false;
    public static event Action OnSceneTransitionEvent;

    private bool sceneTransitioning = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !sceneTransitioning)
        {
            StartTransition();
        }
    }

    public void StartTransition()
    {
        if (_isFinalExit)
        {
            SceneManager.LoadScene("EndCreditsMenu");
        }
        else { StartCoroutine(HandleSceneTransition()); }
    }
    IEnumerator HandleSceneTransition()
    {
        //Scene Transition first 
        sceneTransitioning = true;
        OnSceneTransitionEvent?.Invoke();
        AsyncOperation newSceneLoad = SceneManager.LoadSceneAsync(sceneTarget.SceneName, LoadSceneMode.Single);
        newSceneLoad.allowSceneActivation = true;
        while (!newSceneLoad.isDone)
        {
            yield return null; 
        }
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
        MenuManager.Instance.ScreenFadeIn();
        HUDMessage.Instance.UpdateMessage("Loading...", 1f);
        SoundEffectManager.Instance.Builder
            .SetSound(portalSFX)
            .SetSoundPosition(this.transform.position)
            .ValidateAndPlaySound();
    }
}