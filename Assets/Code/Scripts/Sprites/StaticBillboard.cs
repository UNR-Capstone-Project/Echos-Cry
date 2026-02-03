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
        else
        {
            Debug.Log("Billboard failed: No main camera has been tagged.");
        }
        transform.rotation = cameraTransform.localRotation;

        //if (lockZRotation)
        //{
        //    float cameraXRotation = cameraPos.rotation.eulerAngles.x;
        //    float cameraYRotation = cameraPos.rotation.eulerAngles.y;
        //    transform.rotation = Quaternion.Euler(cameraXRotation, cameraYRotation, 0f);
        //}
        //else
        //{
        //    //transform.LookAt(transform.position + (transform.position - cameraPos.position)); //flip z-axis to face camera
        //}
    }
}