using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI hitQualityText;
    [SerializeField] private GameObject metronomeImage;

    private Material metronomeMaterial;

    void Start()
    {
        //Setup UI with main camera.
        Canvas mCanvas = GetComponent<Canvas>();
        mCanvas.worldCamera = Camera.main;
        mCanvas.planeDistance = 1;

        RawImage image = metronomeImage.GetComponent<RawImage>();
        metronomeMaterial = Instantiate(image.material);
        image.material = metronomeMaterial;
    }

    public void UpdateHitQualityText(string quality)
    {
        hitQualityText.GetComponent<TextMeshProUGUI>().text = quality;
    }

    void Update()
    {
        
    }

    public void FlashOutline(float flashDuration)
    {
        Debug.Log("Flash!");
        StartCoroutine(Flash(flashDuration));
    }

    IEnumerator Flash(float duration)
    {
        metronomeMaterial.SetFloat("_Enabled", 1f);
        yield return new WaitForSeconds(duration);
        metronomeMaterial.SetFloat("_Enabled", 0f);
    }
}
