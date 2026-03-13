using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class BeatUIHandler : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
    [SerializeField] private TextMeshProUGUI hitQualityText;
    [SerializeField] private TextMeshProUGUI offHitDebugText;
    [SerializeField] private Animator _textAnimator;
    [SerializeField] private Animator _hitEffectAnimator1;
    [SerializeField] private Animator _hitEffectAnimator2;
    [SerializeField] private bool showHitText = true;

    void Start()
    {
        //_translator.OnDashEvent += UpdateHitQualityText;
        _translator.OnPrimaryActionEvent += UpdateHitQualityText;
        _translator.OnSecondaryActionEvent += UpdateHitQualityText;
    }

    private void OnDestroy()
    {
        //_translator.OnDashEvent -= UpdateHitQualityText;
        _translator.OnPrimaryActionEvent -= UpdateHitQualityText;
        _translator.OnSecondaryActionEvent -= UpdateHitQualityText;
    }

    private void UpdateHitQualityText(bool isPressed)
    {
        if (!isPressed) return;

        //On Beat Text
        switch (TempoConductor.Instance.CurrentHitQuality)
        {
            case TempoConductor.HitQuality.Excellent:
                hitQualityText.color = new Color(110f / 255f, 44f / 255f, 222f / 255f, 1f); //purple
                break;
            case TempoConductor.HitQuality.Good:
                hitQualityText.color = new Color(47f / 255f, 235f / 255f, 81f / 255f, 1.0f);
                break;
            case TempoConductor.HitQuality.Miss:
                hitQualityText.color = Color.red;
                break;
        }

        //Off Beat Text
        switch (TempoConductor.Instance.CurrentOffHitQuality)
        {
            case TempoConductor.HitQuality.Excellent:
                offHitDebugText.color = new Color(110f / 255f, 44f / 255f, 222f / 255f, 1f); //purple
                break;
            case TempoConductor.HitQuality.Good:
                offHitDebugText.color = new Color(47f / 255f, 235f / 255f, 81f / 255f, 1.0f);
                break;
            case TempoConductor.HitQuality.Miss:
                offHitDebugText.color = Color.red;
                break;
        }

        if (showHitText)
        {
            offHitDebugText.text = TempoConductor.Instance.CurrentOffHitQuality.ToString();
            hitQualityText.text = TempoConductor.Instance.CurrentHitQuality.ToString();
            _textAnimator.SetTrigger("Bounce");
        }

        _hitEffectAnimator1.SetTrigger("Effect");
        _hitEffectAnimator2.SetTrigger("Effect");
    }
}
