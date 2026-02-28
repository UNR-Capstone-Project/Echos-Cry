using UnityEngine;

public class StaticBillboard : MonoBehaviour
{
    public bool lockZRotation = false;

    private Transform cameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        transform.rotation = cameraTransform.localRotation;
    }
}