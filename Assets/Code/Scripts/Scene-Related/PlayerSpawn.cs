using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawn : MonoBehaviour
{
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
        GameObject player = GameObject.FindWithTag("Player");
        //If there are many objects, then find with tag can slow down game a lot.
        //Use a static singleton reference to player.
        if (player != null)
        {
            player.GetComponent<Rigidbody>().position = this.transform.position;
        }
    }
}
