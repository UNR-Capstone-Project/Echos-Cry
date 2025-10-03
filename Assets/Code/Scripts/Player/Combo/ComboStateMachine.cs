using System.Collections;
using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    void EnterNewComboState()
    {
        //CurrentState = 
    }
    void HandleLightAttack()
    {
        if (!_readyForAttackInput) return;
        _currentPlayerInputType = PlayerInputType.LIGHT_ATTACK;
        _readyForAttackInput = false;
        StartCoroutine(InputCooldown());
    }
    void HandleHeavyAttack()
    {
        if(!_readyForAttackInput) return;
        _currentPlayerInputType = PlayerInputType.HEAVY_ATTACK;
        _readyForAttackInput = false;
        StartCoroutine(InputCooldown());
    }

    private IEnumerator InputCooldown()
    {
        yield return new WaitForSeconds(_inputCooldownTimer);
        _readyForAttackInput = true;
    }

    private void Start()
    {
        if (CurrentState == null)
        {
            Debug.LogError("ComboStateMachine's CurrentState is unset and the ComboSystem will be disabled!");
            this.enabled = false;
        }
        if (_inputTranslator == null)
        {
            Debug.LogError("Player inputs not registered to ComboSystem. ComboSystem will be disbaled!");
            this.enabled = false;
        }
    }

    public enum PlayerInputType{
        NONE = 0,
        LIGHT_ATTACK,
        HEAVY_ATTACK
    }
    public ComboState CurrentState = null;
    private PlayerInputType _currentPlayerInputType = PlayerInputType.NONE;
    private bool _readyForAttackInput = true;
    [SerializeField] private float _inputCooldownTimer = 0.5f;
    [SerializeField] private InputTranslator _inputTranslator;
}
