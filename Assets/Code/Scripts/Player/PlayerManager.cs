using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private PlayerComboMeter _playerComboMeter;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerSound _playerSound;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private ComboStateMachine _comboStateMachine;
    [SerializeField] private PlayerAttackHandler _playerAttackHandler;
    [SerializeField] private PlayerSkillManager _playerSkillManager;
    [SerializeField] private PlayerDirection _playerDirection;
    [SerializeField] private PlayerCurrencySystem _playerCurrencySystem;

    private PlayerStateMachine _playerStateMachine;

    private void Awake()
    {
        _playerStateMachine = new PlayerStateMachine(this);
    }
    private void Update()
    {
        _playerStateMachine.Update();
    }

}
