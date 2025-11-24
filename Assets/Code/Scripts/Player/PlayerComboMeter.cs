using System;
using UnityEngine;

public class PlayerComboMeter : MonoBehaviour
{
    //Brain not working taking break
    //TODO:
    //  - Implement combo bar increase based on attack hit quality
    //  - Implement combo bar decrease at a certain rate that pauses based on attack hit quality
    //  - Implement combo multiplier increase based on attack hit quality
    //  - Implement combo multiplier decrease based on time and attack hit quality (maybe just fully reset combo bar if they miss)

    public static void AddToComboMeter(float amount)
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount + (0.125f * _comboMultiplier) * amount, 0, _comboMeterMax);
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }
    public static void SubtractFromComboMeter(float amount)
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount - amount, 0, _comboMeterMax);
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }
    public static void UpdateComboMultiplier()
    {
        _comboMultiplier = Mathf.Clamp(_comboMultiplier * 2, 1, _comboMultiplierMax);
    }
    public static void UpdateHitQualityMultipliers(TempoManager.HIT_QUALITY hit)
    {
        switch (hit)
        {
            case TempoManager.HIT_QUALITY.EXCELLENT:
                _percentOfDamage = 0.25f;
                break;
            case TempoManager.HIT_QUALITY.GOOD:
                _percentOfDamage = 0.175f;
                break;
            //case TempoManager.HIT_QUALITY.BAD:
            //    _percentOfDamage = 0.1f;
            //    break;
        }
    }

    private void DrainComboMeter()
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount - (_comboMeterDrainRate * Time.deltaTime), 0, _comboMeterMax);
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }
    private void DecreaseComboMultiplier()
    {
        _comboMultiplier = Mathf.Clamp(_comboMultiplier - 1, 1, _comboMultiplierMax);
    }

    void Update()
    {
        //if(_comboMeterAmount > 0 && _isDraining) DrainComboMeter();
    }
    void Start()
    {
        _comboMeterAmount = 0;
        _comboMultiplier = 1;
    }

    public static event Action<float, float> OnComboMeterChangeEvent;

    [SerializeField] private float _comboMeterDrainRate = 2.5f;
    private static float _comboMeterMax = 120f;
    private static float _comboMeterAmount;

    private static float _comboMultiplierMax = 16;
    private static float _percentOfDamage = 0.25f;
    private static float _comboMultiplier;

    private bool _isDraining = true;
    private bool _isResetingMultiplier = true;

    public static float ComboMeterAmount { get { return _comboMeterAmount; } }
}
