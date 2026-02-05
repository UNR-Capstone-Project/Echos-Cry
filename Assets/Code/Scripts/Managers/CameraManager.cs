using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;
    public static CameraManager Instance { get { return _instance; } }

    [SerializeField] private static Camera _mainCamera;
    private CinemachineCamera cameraBrain;
    private CinemachineBasicMultiChannelPerlin cinemachinePerlin;

    private float shakeTimer;
    private float startingIntensity;
    private float shakeTimerTotal;
    public static Camera MainCamera { get { return _mainCamera; } }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;

        cameraBrain = gameObject.GetComponentInChildren<CinemachineCamera>();
        cinemachinePerlin = cameraBrain.GetComponent<CinemachineBasicMultiChannelPerlin>();

        _mainCamera = GetComponentInChildren<Camera>();
    }
    private void Start()
    {
        cameraBrain.Target.TrackingTarget = PlayerRef.Transform;
    }

    // This was provided from a tutorial by CodeMonkey on Youtube https://youtu.be/ACf1I27I6Tk?si=Ic2BCVnjAV80Pkvv
    public void ScreenShake(float intensity, float time)
    {
        cinemachinePerlin.AmplitudeGain = intensity;
        startingIntensity = intensity;
        shakeTimer = time;
        shakeTimerTotal = time;
    }

    private void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            cinemachinePerlin.AmplitudeGain = Mathf.Lerp(startingIntensity, 0f, shakeTimer / shakeTimerTotal);
        }
        else
        {
            cinemachinePerlin.AmplitudeGain = 0f;
        }
    }
}
