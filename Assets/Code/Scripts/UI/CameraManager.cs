using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject mPlayer;
    private void Awake()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponentInChildren<CinemachineCamera>().Target.TrackingTarget = mPlayer.transform;
    }
}
