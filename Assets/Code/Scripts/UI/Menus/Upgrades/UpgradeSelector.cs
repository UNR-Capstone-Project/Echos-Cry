using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSelector : MonoBehaviour
{
    [SerializeField] private GameObject _highlight;
    [SerializeField] private TextMeshProUGUI _upgradeNameText;
    [SerializeField] private TextMeshProUGUI _upgradeDescriptionText;
    [SerializeField] private Button _useButton;

    private UpgradeManager.UpgradeType currentUpgrade;

    public void SetUpgrade(UpgradeManager.UpgradeType upgrade)
    {
        Debug.Log("Hello world!");
        currentUpgrade = upgrade;
        _upgradeNameText.text = currentUpgrade.ToString();
    }

    public void UseUpgrade()
    {
        UpgradeManager.Instance.ApplyUpgrade(currentUpgrade);
        UpgradeManager.Instance.Roll();
    }

    public void SetSelectorState(bool state)
    {
        _useButton.interactable = state;
        if (!state)
        { 
            _upgradeNameText.text = "???";
            _upgradeDescriptionText.text = "You need to level up to see this upgrade!";
        }
    }
}
