using UnityEngine;
using UnityEngine.UI;

public class ComboProgressUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float ComboMeterMax = 30f;
    [SerializeField] private Image progressRingImage;

    void Update()
    {
        int currentAttacksHit = playerStats.GetCountAttacksHit();
        if (currentAttacksHit <= ComboMeterMax)
        {
            progressRingImage.fillAmount = currentAttacksHit / ComboMeterMax;
        }
        else
        {
            progressRingImage.fillAmount = 1;
        }   
    }
}
