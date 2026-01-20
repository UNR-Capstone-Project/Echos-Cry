using AudioSystem;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    [SerializeField] private StatsConfig _statsConfig;
    [SerializeField] private ItemDropStrategy _itemDropStrategy;
    [SerializeField] private GameObject _destroyedPrefab;
    [SerializeField] soundEffect _destroySFX;
    [SerializeField] private bool _isDestructable;
    private float _health;
    public float Health { get => _health; set => _health = value; }

    private void CheckHealth()
    {
        if (_health <= 0) HandleObjectDestroyed();
    }

    private void HandleObjectDestroyed()
    {
        Instantiate(_destroyedPrefab, transform.position, transform.rotation);
        if (_itemDropStrategy != null) _itemDropStrategy.Execute(transform);
        soundEffectManager.Instance.Builder
            .setSound(_destroySFX)
            .setSoundPosition(transform.position)
            .ValidateAndPlaySound();
        Destroy(gameObject);
    }

    private void Start()
    {
        if (_isDestructable && _statsConfig != null)
        {
            _health = _statsConfig.maxHealth;
            TickManager.Instance.GetTimer(0.2f).Tick += CheckHealth;
        }

    }
    private void OnDestroy()
    {
        if (_isDestructable && _statsConfig != null)
        {
            TickManager.Instance.GetTimer(0.2f).Tick -= CheckHealth;
        }
    }
}