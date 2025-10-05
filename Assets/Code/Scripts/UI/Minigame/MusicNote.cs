using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MusicNote : MonoBehaviour
{
    public float accuracy = 0.8f;

    private float hitTime = 0f;
    private float prevHitTime = 0f;
    private float fadeDuration = 0.5f;
    private bool isFading = false;

    private float noteDuration;
    private string notePitch;
    private Image UIImage;
    private Color originalColor;

    void Start()
    {
        UIImage = GetComponent<Image>();
        originalColor = UIImage.color;
    }

    void Update()
    {
        if (hitTime >= noteDuration * accuracy && !isFading)
        {
            //Debug.Log("Note was successfully held for: " + hitTime.ToString() + " seconds.");
            GameObject.Find("MinigameCanvas").GetComponent<MinigameManager>().addSuccessfulNoteHit();
            StopAllCoroutines();
            StartCoroutine(FadeDestroy(0.1f, Color.green));
            isFading = true;
        }

        if (hitTime > prevHitTime)
        {
            prevHitTime = hitTime;
            UIImage.color = Color.orange;
        }
        else
        {
            UIImage.color = originalColor;
        }
    }

    public void setDestroyTimer(float destroyTime)
    {
        StartCoroutine(FadeDestroy(destroyTime, Color.red));
    }

    private IEnumerator FadeDestroy(float destroyTime, Color newColor)
    {
        yield return new WaitForSeconds(destroyTime);

        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeDuration);
            float newColorR = Mathf.Lerp(originalColor.r, newColor.r, time / fadeDuration);
            float newColorG = Mathf.Lerp(originalColor.g, newColor.g, time / fadeDuration);
            float newColorB = Mathf.Lerp(originalColor.b, newColor.b, time / fadeDuration);

            UIImage.color = new Color(newColorR, newColorG, newColorB, alpha);
            time += Time.deltaTime;

            yield return null;
        }

        if (hitTime < noteDuration * accuracy)
        {
            GameObject.Find("MinigameCanvas").GetComponent<MinigameManager>().addFailedNoteHit();
        }

        Destroy(gameObject);
    }

    public void addHitTime(float amount)
    {
        hitTime += amount;
        //Debug.Log(hitTime.ToString());
    }

    public void setupNote(string name, float duration, float baseWidth)
    {
        notePitch = name;
        GetComponentInChildren<TextMeshProUGUI>().text = notePitch;

        noteDuration = duration;
        Vector2 newSize = new Vector2(baseWidth * duration, GetComponent<RectTransform>().sizeDelta.y);
        GetComponent<RectTransform>().sizeDelta = newSize;
        GetComponent<BoxCollider2D>().size = newSize;
        GetComponent<BoxCollider2D>().offset = new Vector2(-newSize.x / 2, 0f);
    }
}
