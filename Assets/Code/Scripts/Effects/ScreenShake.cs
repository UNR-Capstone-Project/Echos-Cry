using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    [SerializeField] private float shakeIntensity = 0.6f;
    [SerializeField] private float shakeDuration = 0.5f;
    public void ActivateScreenShake()
    {
        CameraManager.Instance.ScreenShake(shakeIntensity, shakeDuration);
    }
}
