using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager Instance { get { return _instance; } }

    [SerializeField] private Camera _mainCamera;
    public Camera MainCamera { get { return _mainCamera; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;

        gameObject.GetComponentInChildren<CinemachineCamera>().Target.TrackingTarget = PlayerRef.PlayerTransform;
    }
}
