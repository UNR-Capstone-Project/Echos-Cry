using UnityEngine;
using UnityEngine.Pool;

public class DamageLabelManager : Singleton<DamageLabelManager>
{
    private ObjectPool<DamageLabel> damageLabelPopupPool;

    //Find another way to pass damage label to manager
    [SerializeField] private DamageLabel damageLabelPrefab;
    [Range(0.2f, 1.5f), SerializeField] private float _displayLength = 1f;

    protected override void OnAwake()
    {
        damageLabelPopupPool = new ObjectPool<DamageLabel>(
            () =>
            {
                DamageLabel damageLabel = Instantiate(damageLabelPrefab, transform);
                damageLabel.Initialize(_displayLength, this);
                return damageLabel;
            },
            damageLabel => damageLabel.gameObject.SetActive(true),
            damageLabel => damageLabel.gameObject.SetActive(false)
        );
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
    public void ReturnDamageLabelToPool(DamageLabel damageLabel3d)
    {
        damageLabelPopupPool.Release(damageLabel3d);
    }
}