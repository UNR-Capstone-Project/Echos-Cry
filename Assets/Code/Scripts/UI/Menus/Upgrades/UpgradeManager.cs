using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeSelector[] _upgradeSelectors;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _xpRequiredText;
    [SerializeField] private TextMeshProUGUI _pointsAvailableText;

    private int availablePoints = 1;
    public enum UpgradeType
    {
        Speed,
        Strength,
        Defense,
        Attack
    }

    private Dictionary<UpgradeType, string> _upgradeDescriptions = new Dictionary<UpgradeType, string>
    {
        //[UpgradeType.] = "",
        [UpgradeType.Speed] = "This is speed.",
        [UpgradeType.Strength] = "This is strength.",
        [UpgradeType.Defense] = "This is defense.",
        [UpgradeType.Attack] = "This is attack.",
    };

    public static UpgradeManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        PlayerXpSystem.OnLevelUp += AddLevelPoint;
        PlayerXpSystem.OnXPChangeEvent += UpdateInfo;

        Roll();
    }

    private void OnDestroy()
    {
        PlayerXpSystem.OnLevelUp -= AddLevelPoint;
        PlayerXpSystem.OnXPChangeEvent -= UpdateInfo;
    }

    public string GetDescription(UpgradeType upgrade)
    {
        return _upgradeDescriptions.GetValueOrDefault(upgrade);
    }

    private void UpdateInfo(int xp, int xpRequired, int level)
    {
        _xpRequiredText.text = $"[{xp} / {xpRequired}] XP till Level {level}";
        _currentLevelText.text = $"Current Level: {level}";
        UpdateSelectors();
    }

    private void AddLevelPoint(int level)
    {
        availablePoints++;
        UpdateSelectors();
    }

    private void UpdateSelectors()
    {
        bool canUpgrade = availablePoints > 0;

        _pointsAvailableText.text = $"{availablePoints} Upgrade Points to Spend";
        if (canUpgrade)
            _pointsAvailableText.color = Color.white;
        else
            _pointsAvailableText.color = Color.red;

            foreach (UpgradeSelector selector in _upgradeSelectors)
            {
                selector.SetSelectorState(canUpgrade);
            }
    }

    public void Roll()
    {
        Array upgradeValues = Enum.GetValues(typeof(UpgradeType));
        UpgradeType[] currentUpgrades = new UpgradeType[_upgradeSelectors.Length];
        for (int i = 0; i < _upgradeSelectors.Length; i++)
        {
            int randIndex = UnityEngine.Random.Range(0, upgradeValues.Length);
            UpgradeType randomUpgrade = (UpgradeType)upgradeValues.GetValue(randIndex);

            while (currentUpgrades.Contains(randomUpgrade)) //Must have less selectors then enums, otherwise this will become infinite loop!
            {
                randIndex = UnityEngine.Random.Range(0, upgradeValues.Length);
                randomUpgrade = (UpgradeType)upgradeValues.GetValue(randIndex);
            }

            currentUpgrades[i] = randomUpgrade;
        }

        int count = 0;
        foreach (UpgradeType upgrade in currentUpgrades)
        {
            _upgradeSelectors[count].SetUpgrade(upgrade);
            count++;
        }

        UpdateSelectors();
    }
    public void ApplyUpgrade(UpgradeType upgradeType)
    {
        availablePoints--;

        switch (upgradeType)
        {
            case UpgradeType.Speed:
                Debug.Log("Speed upgrade applied.");
                break;
            case UpgradeType.Strength:
                Debug.Log("Strength upgrade applied.");
                break;
            case UpgradeType.Defense:
                Debug.Log("Defense upgrade applied.");
                break;
            case UpgradeType.Attack:
                Debug.Log("Attack upgrade applied.");
                break;
            default:
                Debug.LogWarning("Unknown upgrade type.");
                break;
        }
    }
}
