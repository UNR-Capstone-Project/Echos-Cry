using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ComboProgressNotifier : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _notificationText;
    [SerializeField] private Image _iconImage;
    void Start()
    {
        PlayerComboMeter.OnComboMeterPassiveUnlocked += UpdateNotification;
        UpdateNotification("", null);
    }
    void OnDestroy()
    {
        PlayerComboMeter.OnComboMeterPassiveUnlocked -= UpdateNotification;
    }
    private void UpdateNotification(string text, Sprite icon)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            _iconImage.enabled = false;
            _notificationText.text = "No Passives";
        }
        else
        {
            _iconImage.enabled = true;
            _iconImage.sprite = icon;
            _notificationText.text = text;
        } 
    }
}
