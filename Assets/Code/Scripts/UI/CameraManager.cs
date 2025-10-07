using Unity.Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private GameObject mPlayer;

    void Start()
    {
        mPlayer = GameObject.FindGameObjectWithTag("Player");
        gameObject.GetComponentInChildren<CinemachineCamera>().Target.TrackingTarget = mPlayer.transform;
    }
}
