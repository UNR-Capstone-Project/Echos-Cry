using System.Collections;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class RenderMetronomeManager : MonoBehaviour
{
    [SerializeField] private GameObject pendulumObject;

    private TempoManagerV2 _tempoManager;
    //private Animator pendulumAnimator;
    private float _currentPendulumBeatTime = 0;
    private float _swingTime = 0;

    private void Awake()
    {
        _tempoManager = GameObject.Find("TempoManager").GetComponent<TempoManagerV2>();
        //pendulumAnimator = pendulumObject.GetComponent<Animator>();
    }
    void Start()
    {
        _tempoManager.UpdateTempoEvent += SetPendulumSwingTime;
    }
    private void Update()
    {
        _currentPendulumBeatTime += Time.deltaTime;

        float angle = Mathf.Sin((_currentPendulumBeatTime / _swingTime) * Mathf.PI * 2f) * 30f;
        pendulumObject.transform.localRotation = Quaternion.Euler(0f, 0f, angle);

        if (_currentPendulumBeatTime >= _swingTime) _currentPendulumBeatTime -= _swingTime;
    }
    private void OnDestroy()
    {
        _tempoManager.UpdateTempoEvent -= SetPendulumSwingTime;
    }

    public void SetPendulumSwingTime(float timeBetweenBeats)
    {
        _swingTime = timeBetweenBeats * 2f;
    }
}
