using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

public class MetronomeCanvas : MonoBehaviour
{
    [SerializeField] private InputTranslator _translator;
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

        TempoConductor.Instance.BeatTickEvent += FlashOutline;

        _translator.OnDashEvent += UpdateHitQualityText;
        _translator.OnPrimaryActionEvent += UpdateHitQualityText;
        _translator.OnSecondaryActionEvent += UpdateHitQualityText;
    }

    private void OnDestroy()
    {
        if(TempoConductor.Instance != null) TempoConductor.Instance.BeatTickEvent -= FlashOutline;

        _translator.OnDashEvent -= UpdateHitQualityText;
        _translator.OnPrimaryActionEvent -= UpdateHitQualityText;
        _translator.OnSecondaryActionEvent -= UpdateHitQualityText;
    }

    private void UpdateHitQualityText(bool isPressed)
    {
        if (!isPressed) return;
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
        hitQualityText.text = TempoConductor.Instance.CurrentHitQuality.ToString();
    }

    public void FlashOutline()
    {
        if (MusicManager.Instance.SongCurrentlyPlaying() && gameObject.activeInHierarchy)
        {
            StartCoroutine(Flash(flashDuration));
        }
    }

    IEnumerator Flash(float duration)
    {
        metronomeMaterial.SetFloat("_Enabled", 1f);
        yield return new WaitForSeconds(duration);
        metronomeMaterial.SetFloat("_Enabled", 0f);
    }
}
