using System.Linq;
using System.Security.Cryptography;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class playerHealthManager : MonoBehaviour
{

    [SerializeField] private UnityEngine.UI.Image[] allHealthPoints = new UnityEngine.UI.Image[17];

    void Awake()
    {
        UnityEngine.UI.Image[] allComponents = GetComponentsInChildren<UnityEngine.UI.Image>();

        allHealthPoints = allComponents.Where(img => img.gameObject.name.StartsWith("HP")).OrderBy(img => int.Parse(img.gameObject.name.Substring(2))).ToArray();

        
    }

    void OnEnable()
    {
        playerHealthController.onPlayerHealthChange += updateHealth;
        playerHealthController.onPlayerDeath += updateHealthOnDeath;
    }

    void OnDisable()
    {
        playerHealthController.onPlayerHealthChange -= updateHealth;
        playerHealthController.onPlayerDeath -= updateHealthOnDeath;
    }

    private void updateHealth(float updatedValue)
    {
        Debug.Log("Updating Health UI from health change");
        int roundedValue = Mathf.RoundToInt(updatedValue);
        
        if (roundedValue >= 1 && roundedValue <= 17)
        {
            //int damagedHitpoints = 17 - roundedValue;
            for (int i = 0; i < allHealthPoints.Length; i++)
            {
                allHealthPoints[i].enabled = (i < roundedValue);
            }
        }
    }

    private void updateHealthOnDeath() {
        
    }
}
