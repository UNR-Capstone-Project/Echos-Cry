using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// SceneTriggerManager is for loading scenes with a clear distinction between each other
/// eg. loading from a level to a completely different level, or boss arena, etc 
/// </summary>
public class SceneTriggerManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 playerTargetSpawn;
    [SerializeField] private SceneField sceneTarget;
    [SerializeField] private SceneField sceneOrigin;
    [SerializeField] private bool doesPersistInScene;
    
    private void updatePlayerPosition()
    {
        player.transform.position = new Vector3(playerTargetSpawn.x, playerTargetSpawn.y, playerTargetSpawn.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            loadScene();
            unloadScene();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            if (!doesPersistInScene)
            {
                Destroy(gameObject);
            }
        }
    }

    private void loadScene()
    {
        if (sceneOrigin == null || sceneTarget == null)
        {
            Debug.Log("Scene target or origin is not filled.");
            return;
        }
        else
        {
            if (sceneTarget != sceneOrigin)
            {
                if (SceneManager.sceneCount > 0)
                {
                    for (int i = 0; i < SceneManager.sceneCount; ++i)
                    {
                        string nameCheck = SceneManager.GetSceneAt(i).name;
                        if (nameCheck == sceneTarget.SceneName)
                        {
                            return; //prevents loading the same scene more than once
                        }
                    }
                }
                SceneManager.LoadSceneAsync(sceneTarget.SceneName, LoadSceneMode.Additive);
                updatePlayerPosition();
            }
        }
    }

    private void unloadScene()
    {
        if (sceneOrigin == null || sceneTarget == null)
        {
            Debug.Log("Scene target or origin is not filled.");
            return;
        }
        else
        {
            if (sceneOrigin.SceneName == "PersistentScene")
            {
                return;
            }
            else
            {
                SceneManager.UnloadSceneAsync(sceneOrigin.SceneName);
            }
        }

    }
    

}
