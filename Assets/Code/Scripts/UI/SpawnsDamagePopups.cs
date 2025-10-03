using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class SpawnsDamagePopups : MonoBehaviour
{
    public static SpawnsDamagePopups Instance { get; private set; }
    private ObjectPool<DamageLabel> damageLabelPopupPool;

    [SerializeField] private DamageLabel damageLabelPrefab;
    [Range(0.2f, 1.5f), SerializeField] public float displayLength = 1f;
    private Camera mainCamera;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        damageLabelPopupPool = new ObjectPool<DamageLabel>(
            () =>
            {
                DamageLabel damageLabel = Instantiate(damageLabelPrefab, transform);
                damageLabel.Initialize(displayLength, this);
                return damageLabel;
            },
            damageLabel => damageLabel.gameObject.SetActive(true),
            damageLabel => damageLabel.gameObject.SetActive(false)
        );
        
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        mainCamera = Camera.main;
    }

    public void DamageDone(float damage, Vector3 position)
    {
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(position);
        screenPosition.z = 0;
        bool direction = screenPosition.x < Screen.width * 0.5f;

        SpawnDamagePopup(damage, screenPosition, direction);
    }

    private void SpawnDamagePopup(float damage, Vector3 position, bool direction)
    {
        DamageLabel damageLabel = damageLabelPopupPool.Get();
        damageLabel.Display(damage, position, direction);
    }

    public void ReturnDamageLabelToPool(DamageLabel damageLabel3d)
    {
        damageLabelPopupPool.Release(damageLabel3d);
    }
}
