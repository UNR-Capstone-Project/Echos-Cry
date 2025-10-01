using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hitQualityText;
    [SerializeField] private TextMeshProUGUI playerHealthText;
    [SerializeField] private GameObject metronomeImage;
    [SerializeField] private float flashDuration = 0.1f;

    private Player mPlayer;
    private TempoManagerV2 tempoManager;
    private Material metronomeMaterial;

    void Start()
    {
        //Setup UI with main camera.
        Canvas mCanvas = GetComponent<Canvas>();
        mCanvas.worldCamera = Camera.main;
        mCanvas.planeDistance = 1;

        //Setup metronome image
        RawImage image = metronomeImage.GetComponent<RawImage>();
        metronomeMaterial = Instantiate(image.material);
        image.material = metronomeMaterial;

        //Setup Tempo Manager
        tempoManager = GameObject.Find("TempoManager").GetComponent<TempoManagerV2>();
       
        tempoManager.BeatTickEvent += FlashOutline;
        tempoManager.UpdateHitQualityEvent += UpdateHitQualityText;

        //Setup player reference
        mPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
        mPlayer.HealthUpdateEvent += UpdateHealthBar;
        UpdateHealthBar(mPlayer.getHealth()); //Init starting health
    }
    private void OnDestroy()
    {
        tempoManager.BeatTickEvent -= FlashOutline;
        tempoManager.UpdateHitQualityEvent -= UpdateHitQualityText;
    }

    public void UpdateHealthBar(float health)
    {
        playerHealthText.GetComponent<TextMeshProUGUI>().text = "HP: " + health.ToString();
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
