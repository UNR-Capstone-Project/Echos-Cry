using TMPro;
using UnityEngine;
using DG.Tweening;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private DoubleFloatEventChannel eventChannel;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI livesRemainingText;

    // Variables Provided from here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 5:00
    private float lerpTimer;
    public float chipSpeed = 3f;
    private float hFraction = 0f;
    [SerializeField] private UnityEngine.UI.Image frontHealthBar;
    [SerializeField] private UnityEngine.UI.Image backHealthBar;
    [SerializeField] private Color frontFlashColor;

    void Start()
    {
        if(eventChannel != null) eventChannel.Channel += UpdateHealth;
        GameManager.OnPlayerDeathEvent += UpdateLives;
        UpdateLives();
    }

    void OnDestroy()
    {
        if (eventChannel != null) eventChannel.Channel -= UpdateHealth;
        GameManager.OnPlayerDeathEvent -= UpdateLives;
    }

    private void UpdateLives()
    {
        livesRemainingText.text = $"Lives Remaining: {GameManager.PlayerLives}";
    }

    private void UpdateHealth(float currentHealth, float maxHealth)
    {
        healthText.text = currentHealth.ToString() + "/" + maxHealth.ToString();
        hFraction = currentHealth / maxHealth;
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        //lerpTimer = 0f;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            //backHealthBar.color = Color.red;
            frontHealthBar.DOKill();
            DOTween.To(() => frontHealthBar.color, x => { frontFlashColor = x;}, frontFlashColor, 0.10f).SetEase(Ease.OutQuad);
            backHealthBar.DOKill();
            DOTween.To(() => backHealthBar.fillAmount, x => backHealthBar.fillAmount = x, hFraction, chipSpeed).SetEase(Ease.OutQuad);
        }
        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            frontHealthBar.DOKill();
            DOTween.To(() => frontHealthBar.fillAmount, x => frontHealthBar.fillAmount = x, hFraction, chipSpeed).SetEase(Ease.OutQuad);
        }

    }

    private void Update()
    {
        /*
        //Code Section Provided Here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 13:19
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.green;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }*/
    }
}
