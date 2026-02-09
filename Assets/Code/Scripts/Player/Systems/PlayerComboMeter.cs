using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComboMeter : MonoBehaviour
{
    //-----------------------------------------
    // Combo Variables
    //-----------------------------------------
    [SerializeField] private InputTranslator _inputTranslator;

    private float _comboMeterDrainRate = 10f;
    private float _comboDrainDelay = 4f; //ISSUE: This will not be dynamically adjusted with new time between beats if the tempo changes.
    private float _comboMeterAmount = 0;
    private float _comboBaseIncrease = 4f;
    private float _comboGoodRate = 1.2f;
    private float _comboExcellentRate = 1.5f;
    private float _comboMeterMax = 120f;
    private bool _isDraining = false;

    public static event Action<float, float> OnComboMeterChangeEvent;
    public float ComboMeterAmount { get { return _comboMeterAmount; } }

    public enum MeterState
    {
        Starting,
        OneThird,
        TwoThirds,
        Full
    }
    private static MeterState _currentMeterState = MeterState.Starting;
    public static MeterState CurrentMeterState { get { return _currentMeterState; } }
    //-----------------------------------------

    private void Update()
    {      
        if (_isDraining)
            DrainComboMeter();
    }

    public void AddToComboMeter(TempoConductor.HitQuality hit)
    {
        switch (hit)
        {
            case TempoConductor.HitQuality.Excellent:
                _comboMeterAmount = Mathf.Clamp(_comboMeterAmount + _comboBaseIncrease * _comboExcellentRate, 0, _comboMeterMax);
                break;
            case TempoConductor.HitQuality.Good:
                _comboMeterAmount = Mathf.Clamp(_comboMeterAmount + _comboBaseIncrease * _comboGoodRate, 0, _comboMeterMax);
                break;
        }

        _isDraining = false;
        if (_comboMeterAmount > 0f)
        {
            StopAllCoroutines();
            StartCoroutine(DrainResetWait());
        }
        
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
        UpdateComboMeterState();
    }
    public void SubtractFromComboMeter(float amount)
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount - amount, 0, _comboMeterMax);
        
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
        UpdateComboMeterState();
    }

    private void UpdateComboMeterState()
    {
        float progress = _comboMeterAmount / _comboMeterMax;
        float oneThird = .33f;
        float twoThirds = .66f;

        if (progress < oneThird)
            _currentMeterState = MeterState.Starting;
        else if (progress >= oneThird && progress < twoThirds)
            _currentMeterState = MeterState.OneThird;
        else if (progress >= twoThirds && progress < 1f)
            _currentMeterState = MeterState.TwoThirds;
        else
            _currentMeterState = MeterState.Full;
    }

    private IEnumerator DrainResetWait()
    {
        yield return new WaitForSeconds(_comboDrainDelay);
        _isDraining = true;
    }

    private void DrainComboMeter()
    {
        _comboMeterAmount = Mathf.Clamp(_comboMeterAmount - (_comboMeterDrainRate * Time.deltaTime), 0, _comboMeterMax);

        UpdateComboMeterState();
        OnComboMeterChangeEvent?.Invoke(_comboMeterAmount, _comboMeterMax);
    }
}

[CreateAssetMenu(menuName = "Echo's Cry/Player Data/Player Combo Meter Config")]
public class ComboMeterConfig : ScriptableObject
{

}
