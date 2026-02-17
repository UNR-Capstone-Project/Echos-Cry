using System.Diagnostics.Tracing;
using TMPro;
using UnityEngine;

public class PlayerDashBar : MonoBehaviour
{
    private float lerpTimer;
    public float chipSpeed = 3f;
    private float hFraction = 0f;
    [SerializeField] private UnityEngine.UI.Image _frontBar;
    [SerializeField] private UnityEngine.UI.Image _backBar;
    [SerializeField] private DoubleIntEventChannel _dashUpdateChannel;

    private void OnEnable()
    {
        _dashUpdateChannel.Channel += UpdateBar;
    }
    private void OnDisable()
    {
        _dashUpdateChannel.Channel -= UpdateBar;
    }

    private void UpdateBar(int dashCount, int dashMax)
    {
        hFraction = (float)dashCount / (float)dashMax; //ISSUE: Need to get this stat from player!
        lerpTimer = 0f;
    }

    private void Update()
    {
        //Code Section Provided Here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 13:19
        float fillF = _frontBar.fillAmount;
        float fillB = _backBar.fillAmount;

        if (fillB > hFraction)
        {
            _frontBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            _backBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            _backBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            _frontBar.fillAmount = Mathf.Lerp(fillF, _backBar.fillAmount, percentComplete);
        }
    }
}