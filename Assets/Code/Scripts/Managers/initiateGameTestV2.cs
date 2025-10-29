using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class initiateGameTestV2 : MonoBehaviour
{
    //[SerializeField] private GameObject mainManager;
    //[SerializeField] private GameObject mainCanvas;
    //[SerializeField] private Volume mainGlobalVolume;
    //[SerializeField] private Light mainDirectionalLight;

    [Header("Scenes to load in")]
    [SerializeField] private SceneField persistentScene;
    [SerializeField] private SceneField firstLevel;

    //private List<AsyncOperation> scenesLoaded = new List<AsyncOperation>();

    void Start()
    {
        //bindObjects();

        SceneManager.LoadSceneAsync(persistentScene); //load scene in bg
        SceneManager.LoadScene(firstLevel, LoadSceneMode.Additive);
        //ISSUE: Show loading screen!


    }

    private void bindObjects()
    {
        //Bind objects to prefabs if needed

    }

    private void createObjects()
    {
        Debug.Log("Function here will be rewritten later if needing to initialize 3rd party services/other services etc. Or tweak object settings after instantiation.");
    }

    private void backgroundGamePreparations()
    {

        Debug.Log("Function here will be rewritten later when needing to load heavy objects async like through Resources, Asset Bundles, or Addressables. Or to move positions of objects around or things of that nature. ");
    }

    //based on the total scenes loaded successfully from scenesLoaded
    private IEnumerator progressLoadingScreen()
    {
        yield return null;
    }
}
