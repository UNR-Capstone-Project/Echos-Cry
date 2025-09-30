using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hitQualityText;
    [SerializeField] private GameObject metronomeImage;
    [SerializeField] private float flashDuration = 0.1f;

    private TempoManagerV2 tempoManager;

    private Material metronomeMaterial;

    private void Awake()
    {
        tempoManager = GameObject.Find("TempoManager").GetComponent<TempoManagerV2>();
    }
    void Start()
    {
        //Setup UI with main camera.
        Canvas mCanvas = GetComponent<Canvas>();
        mCanvas.worldCamera = Camera.main;
        mCanvas.planeDistance = 1;

        RawImage image = metronomeImage.GetComponent<RawImage>();
        metronomeMaterial = Instantiate(image.material);
        image.material = metronomeMaterial;

        tempoManager.BeatTickEvent += FlashOutline;
        tempoManager.UpdateHitQualityEvent += UpdateHitQualityText;
    }
    private void OnDestroy()
    {
        tempoManager.BeatTickEvent -= FlashOutline;
        tempoManager.UpdateHitQualityEvent -= UpdateHitQualityText;
    }
    public void UpdateHitQualityText(TempoManagerV2.HIT_QUALITY quality)
    {
        hitQualityText.GetComponent<TextMeshProUGUI>().text = quality.ToString();
    }

    public void FlashOutline()
    {
        //Debug.Log("Flash!");
        StartCoroutine(Flash(flashDuration));
    }

    IEnumerator Flash(float duration)
    {
        metronomeMaterial.SetFloat("_Enabled", 1f);
        yield return new WaitForSeconds(duration);
        metronomeMaterial.SetFloat("_Enabled", 0f);
    }
}
