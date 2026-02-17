using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Player Systems")]
    [SerializeField] PlayerMovement _movement;
    [SerializeField] HealthSystem _health;

    [Header("Event Channels (Subscribers)")]
    [SerializeField] EventChannel _moveSpeedChannel;
    [SerializeField] EventChannel _dashSpeedChannel;
    [SerializeField] EventChannel _healthChannel;
    [SerializeField] EventChannel _armorChannel;
    [SerializeField] EventChannel _regenHealthChannel;
    [SerializeField] EventChannel _dashCountChannel;
    [SerializeField] EventChannel _dashCooldownChannel;
    [SerializeField] EventChannel _dashAttackChannel;

    private bool _dashAttackEnabled = false;
    public bool DashAttackEnabled { get => _dashAttackEnabled; }

    private void OnEnable()
    {
        if (_healthChannel != null) _healthChannel.Channel += UpgradeMaxHealth;
        if (_armorChannel != null) _armorChannel.Channel += UpgradeMaxArmor;
        if (_regenHealthChannel != null) _regenHealthChannel.Channel += UpgradeHealthRegen;
        if (_moveSpeedChannel != null) _moveSpeedChannel.Channel += UpgradeMoveSpeed;
        if (_dashSpeedChannel != null) _dashSpeedChannel.Channel += UpgradeDashSpeed;
        if(_dashCooldownChannel != null) _dashCooldownChannel.Channel += UpgradeDashCooldown;
        if(_dashCountChannel != null) _dashCountChannel.Channel += UpgradeDashCount;
        if (_dashAttackChannel != null) _dashAttackChannel.Channel += UpgradeDashAttack;
    }
    private void OnDisable()
    {
        if (_healthChannel != null) _healthChannel.Channel -= UpgradeMaxHealth;
        if (_armorChannel != null) _armorChannel.Channel -= UpgradeMaxArmor;
        if (_regenHealthChannel != null) _regenHealthChannel.Channel -= UpgradeHealthRegen;
        if (_moveSpeedChannel != null) _moveSpeedChannel.Channel -= UpgradeMoveSpeed;
        if (_dashSpeedChannel != null) _dashSpeedChannel.Channel -= UpgradeDashSpeed;
        if (_dashCooldownChannel != null) _dashCooldownChannel.Channel -= UpgradeDashCooldown;
        if (_dashCountChannel != null) _dashCountChannel.Channel -= UpgradeDashCount;
        if (_dashAttackChannel != null) _dashAttackChannel.Channel -= UpgradeDashAttack;
    }

    //Currently using unmutable variables but will eventually change to handle configuration or scaling upgrades eventually
    void UpgradeDashSpeed()
    {
        if (_movement != null) _movement.DashSpeed += 0.25f;
    }
    void UpgradeDashCount()
    {
        if (_movement != null) _movement.DashCount++;
        if (_movement != null) _movement.DashMaxCount++;
    }
    void UpgradeDashCooldown()
    {
        if (_movement != null) _movement.DashCooldown -= 0.05f;
    }
    void UpgradeMoveSpeed()
    {
        if (_movement != null) _movement.MoveSpeed += 0.25f;
    }
    void UpgradeMaxHealth()
    {
        if (_health != null)
        {
            _health.MaxHealth += 5f;
            _health.CurrentHealth += 5f;
        }
    }
    void UpgradeMaxArmor()
    {
        if (_health != null)
        {
            _health.MaxArmor += 10f;
            _health.CurrentArmor += 10f;
        }
    }
    void UpgradeHealthRegen()
    {
        if (_health != null)
        {
            _health.RegenHealthAmount++;
            _health.EnableHealthRegen();
        }    
    }
    void UpgradeDashAttack()
    {
        _dashAttackEnabled = true;
    }
}
