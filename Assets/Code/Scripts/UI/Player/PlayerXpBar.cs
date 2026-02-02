using TMPro;
using UnityEngine;

public class PlayerXpBar : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI xpText;

    // Variables Provided from here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 5:00
    private float lerpTimer;
    public float chipSpeed = 3f;
    private float hFraction = 0f;
    [SerializeField] private UnityEngine.UI.Image frontXpBar;
    [SerializeField] private UnityEngine.UI.Image backXpBar;
    [SerializeField] private TextMeshProUGUI levelText;

    void Awake()
    {
        UpdateXp(0, 100, 1);
    }

    void Start()
    {
        PlayerXpSystem.OnXPChangeEvent += UpdateXp;
    }

    void OnDestroy()
    {
        PlayerXpSystem.OnXPChangeEvent -= UpdateXp;
    }

    private void UpdateXp(int xp, int xpRequired, int level)
    {
        xpText.text = $"{xp} / {xpRequired}";
        levelText.text = level.ToString();
        hFraction = (float)xp / xpRequired;
        lerpTimer = 0f;
    }

    private void Update()
    {
        //Code Section Provided Here: https://youtu.be/CFASjEuhyf4?si=ri_WpIV1OxgtQdgp at 13:19
        float fillF = frontXpBar.fillAmount;
        float fillB = backXpBar.fillAmount;

        if (fillB > hFraction)
        {
            frontXpBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backXpBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backXpBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontXpBar.fillAmount = Mathf.Lerp(fillF, backXpBar.fillAmount, percentComplete);
        }
    }
}