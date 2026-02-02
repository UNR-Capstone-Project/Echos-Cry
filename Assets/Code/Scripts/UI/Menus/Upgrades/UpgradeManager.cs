using System;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField] private UpgradeSelector[] _upgradeSelectors;
    [SerializeField] private TextMeshProUGUI _currentLevelText;
    [SerializeField] private TextMeshProUGUI _xpRequiredText;
    [SerializeField] private TextMeshProUGUI _pointsAvailableText;
    [SerializeField] private InputTranslator _inputTranslator;

    private int availablePoints = 1;
    public enum UpgradeType
    {
        Speed,
        Strength,
        Defense,
        Attack
    }

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


    private void UpdateInfo(int xp, int xpRequired, int level)
    {
        _xpRequiredText.text = $"[{xp} / {xpRequired} XP till Level {level}";
        _currentLevelText.text = $"Current Level: {level}";
    }

    private void AddLevelPoint(int level)
    {
        availablePoints++;
        _pointsAvailableText.text = $"{availablePoints} Upgrade Points to Spend";
        UpdateSelectors();
    }

    private void UpdateSelectors()
    {
        _pointsAvailableText.text = $"{availablePoints} Upgrade Points to Spend";

        foreach (UpgradeSelector selector in _upgradeSelectors)
        {
            selector.SetSelectorState(availablePoints > 0);
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
            default:
                Debug.LogWarning("Unknown upgrade type.");
                break;
        }
    }

    public void BackButton()
    {
        MenuManager.Instance.DisablePauseMenu();
        _inputTranslator.PlayerInputs.Gameplay.Enable();
        _inputTranslator.PlayerInputs.PauseMenu.Disable();
    }
}
