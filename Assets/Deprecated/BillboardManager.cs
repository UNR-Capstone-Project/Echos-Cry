using UnityEngine;
using UnityEngine.TextCore;

public class BillboardManager : MonoBehaviour
{
    private GameObject[] _billboardTransforms;
    private Transform cameraPos;
    void Start()
    {
        if (Camera.main != null)
        {
            cameraPos = Camera.main.transform;


            _billboardTransforms = GameObject.FindGameObjectsWithTag("Billboard");
            float cameraXRotation = cameraPos.rotation.eulerAngles.x;
            float cameraYRotation = cameraPos.rotation.eulerAngles.y;
            float cameraZRotation = cameraPos.rotation.eulerAngles.z;

            foreach (GameObject gameObjects in _billboardTransforms)
            {
                gameObjects.transform.rotation = Quaternion.Euler(cameraXRotation, 45f, 0f);
            }
        }
        else
        {
            Debug.Log("Main camera was not found!");
        }
    }
}
