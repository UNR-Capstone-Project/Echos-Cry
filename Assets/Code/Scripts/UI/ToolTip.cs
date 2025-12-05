using System.Collections;
using TMPro;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    public string text;
    public float displayTime = 2f;
    private float fadeDuration = 0.4f;

    void Start()
    {
        textMeshPro.text = text;
        GetComponent<CanvasGroup>().alpha = 0f;
        StartCoroutine(TipTimer(displayTime));
    }

    IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }

    IEnumerator TipTimer(float time)
    {
        yield return FadeIn();
        yield return new WaitForSeconds(time);
        yield return FadeOut();
    }
}
