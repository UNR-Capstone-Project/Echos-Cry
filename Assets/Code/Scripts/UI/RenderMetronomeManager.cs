using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RenderMetronomeManager : MonoBehaviour
{
    [SerializeField] private GameObject pendulumObject;

    private TempoManagerV2 _tempoManager;
    private Animator pendulumAnimator;

    private void Awake()
    {
        _tempoManager = GameObject.Find("TempoManager").GetComponent<TempoManagerV2>();
        pendulumAnimator = pendulumObject.GetComponent<Animator>();
    }
    void Start()
    {
        _tempoManager.UpdateTempoEvent += SetPendulumSpeed;
    }
    private void OnDestroy()
    {
        _tempoManager.UpdateTempoEvent -= SetPendulumSpeed;
    }

    public void SetPendulumSpeed(float angle)
    {
        pendulumObject.transform.localRotation = Quaternion.Euler(0f, 0f, angle);
    }
}
