using UnityEngine;
using UnityEngine.UI;

public class ComboProgressUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private int ComboMeterMax = 30;
    [SerializeField] private Image progressRingImage;

    void Update()
    {
        int currentAttacksHit = playerStats.GetCountAttacksHit();
        Debug.Log(currentAttacksHit.ToString());
        progressRingImage.fillAmount = currentAttacksHit / ComboMeterMax;
    }
}
