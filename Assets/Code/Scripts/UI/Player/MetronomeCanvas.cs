using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MetronomeCanvas : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hitQualityText;
    [SerializeField] private GameObject      metronomeImageObject;
    [SerializeField] private float           flashDuration = 0.1f;
    
    private RawImage _metronomeImage;
    private Canvas   _mainCanvas;
    private Material metronomeMaterial;

    private void Awake()
    {
        _mainCanvas = GetComponent<Canvas>();
        _metronomeImage = metronomeImageObject.GetComponent<RawImage>();
        metronomeMaterial = Instantiate(_metronomeImage.material);
    }
    void Start()
    {
        //Setup UI with main camera.
        _mainCanvas.worldCamera = Camera.main;
        _mainCanvas.planeDistance = 1;

        //Setup metronome image
        _metronomeImage.material = metronomeMaterial;

        TempoManager.BeatTickEvent += FlashOutline;

        InputTranslator.OnDashEvent += UpdateHitQualityText;
        InputTranslator.OnLightAttackEvent += UpdateHitQualityText;
        InputTranslator.OnHeavyAttackEvent += UpdateHitQualityText;
    }
    private void OnDestroy()
    {
        TempoManager.BeatTickEvent -= FlashOutline;

        InputTranslator.OnDashEvent -= UpdateHitQualityText;
        InputTranslator.OnLightAttackEvent -= UpdateHitQualityText;
        InputTranslator.OnHeavyAttackEvent -= UpdateHitQualityText;
    }

    public void UpdateHitQualityText()
    {
        hitQualityText.text = TempoManager.CurrentHitQuality.ToString();
    }

    public void FlashOutline()
    {
        StartCoroutine(Flash(flashDuration));
    }

    IEnumerator Flash(float duration)
    {
        metronomeMaterial.SetFloat("_Enabled", 1f);
        yield return new WaitForSeconds(duration);
        metronomeMaterial.SetFloat("_Enabled", 0f);
    }
}
