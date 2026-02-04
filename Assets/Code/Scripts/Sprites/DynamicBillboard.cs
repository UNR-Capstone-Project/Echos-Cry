using UnityEngine;

public class DynamicBillboard : MonoBehaviour
{
    private Transform cameraTransform;
    private void Start()
    {
        if(Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }
    private void LateUpdate()
    {
        transform.rotation = cameraTransform.localRotation;
    }
}

