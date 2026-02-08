using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComboMeter : MonoBehaviour
{
    //Brain not working taking break
    //TODO:
    //  - Implement combo bar increase based on attack hit quality
    //  - Implement combo bar decrease at a certain rate that pauses based on attack hit quality
    //  - Implement combo multiplier increase based on attack hit quality
    //  - Implement combo multiplier decrease based on time and attack hit quality (maybe just fully reset combo bar if they miss)

    public enum MeterState
    {
        Starting,
        OneThird,
        TwoThirds,
        Full
    }
    private static MeterState _currentMeterState = MeterState.Starting;
    public static MeterState CurrentMeterState { get { return _currentMeterState; } }

    public void AddToComboMeter(float count)
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount + (_percentOfDamage * _comboMultiplier) * count * _comboBaseRate, 0, _comboMeterMax);
        UpdateComboMeterState();
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }
    public void SubtractFromComboMeter(float amount)
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount - amount, 0, _comboMeterMax);
        UpdateComboMeterState();
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }

    private void UpdateComboMeterState()
    {
        float progress = _comboMeterAmount / _comboMeterMax;
        float oneThird = 1f / 3f;
        float twoThirds = 2f / 3f;

        if (progress < oneThird)
            _currentMeterState = MeterState.Starting;
        else if (progress >= oneThird && progress < twoThirds)
            _currentMeterState = MeterState.OneThird;
        else if (progress >= twoThirds && progress != 1f)
            _currentMeterState = MeterState.TwoThirds;
        else
            _currentMeterState = MeterState.Full;
    }

    public void UpdateComboMultiplier()
    {
        _comboMultiplier = Mathf.Clamp(_comboMultiplier * _comboMultiplierRate, 1, _comboMultiplierMax);
        OnComboMultiplierChangeEvent?.Invoke(_comboMultiplier);
    }
    public void ResetComboMultiplier()
    {
        if (TempoConductor.Instance.CurrentHitQuality == TempoConductor.HitQuality.Miss)
        {
            _comboMultiplier = 1;
            OnComboMultiplierChangeEvent?.Invoke(_comboMultiplier);
        }
    }
    public void UpdateHitQualityMultipliers(TempoConductor.HitQuality hit)
    {
        switch (hit)
        {
            case TempoConductor.HitQuality.Excellent:
                _percentOfDamage = 0.03125f;
                _comboMultiplierRate = 2;
                break;
            case TempoConductor.HitQuality.Good:
                _percentOfDamage = 0.03125f;
                _comboMultiplierRate = 1;
                break;
        }
    }
    private void DrainComboMeter()
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount - (_comboMeterDrainRate * Time.deltaTime), 0, _comboMeterMax);
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }

    void Start()
    {
        _comboMeterAmount = 0;
        _comboMultiplier = 1;
    }

    [SerializeField] private InputTranslator _inputTranslator;

    public static event Action<float, float> OnComboMeterChangeEvent;
    public static event Action<float> OnComboMultiplierChangeEvent;

    [SerializeField] private float _comboMeterDrainRate = 2.5f;
    private float _comboMeterMax = 120f;
    private float _comboMeterAmount;

    private float _comboBaseRate = 20f;
    private float _comboMultiplierMax = 16;
    private float _percentOfDamage = 0.25f;
    private float _comboMultiplierRate = 1f;
    private float _comboMultiplier;

    public float ComboMeterAmount { get { return _comboMeterAmount; } }
}

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Combo Meter Config")]
public class ComboMeterConfig : ScriptableObject
{

}
