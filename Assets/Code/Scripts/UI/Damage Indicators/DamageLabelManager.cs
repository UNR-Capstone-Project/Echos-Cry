using UnityEngine;
using UnityEngine.Pool;
using System.Collections.Generic;

public class DamageLabelManager : NonSpawnableSingleton<DamageLabelManager>
{
    private ObjectPool<DamageLabel> damageLabelPopupPool;
    private List<DamageLabel> activeDamageLabels;

    //Find another way to pass damage label to manager
    [SerializeField] private DamageLabel damageLabelPrefab;
    [Range(0.2f, 1.5f), SerializeField] private float _displayLength = 1f;

    protected override void OnAwake()
    {
        damageLabelPopupPool = new ObjectPool<DamageLabel>(
            () =>
            {
                DamageLabel damageLabel = Instantiate(damageLabelPrefab, transform);
                damageLabel.Initialize(_displayLength, damageLabelPopupPool);
                return damageLabel;
            },
            damageLabel =>
            {
                damageLabel.gameObject.SetActive(true);
                activeDamageLabels.Add(damageLabel);
            },
            damageLabel =>
            {
                damageLabel.gameObject.SetActive(false);
                activeDamageLabels.Remove(damageLabel);
            }
        );
        activeDamageLabels = new();
    }

    private void GetAndDisplayPopup(float damage, Vector3 position, bool direction, Color color)
    {
        DamageLabel damageLabel = damageLabelPopupPool.Get();
        damageLabel.Display(damage, position, direction, color);
    }

    public void SpawnPopup(float damage, Vector3 position, Color color)
    {
        Vector3 screenPosition = CameraManager.MainCamera.WorldToScreenPoint(position);
        screenPosition.z = 0;
        bool direction = screenPosition.x < Screen.width * 0.5f;

        GetAndDisplayPopup(damage, screenPosition, direction, color);
    }
}