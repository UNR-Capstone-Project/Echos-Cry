using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerComboMeter : MonoBehaviour
{
    // TODO:
    //  - Implement combo bar increase based on attack hit quality
    //  - Implement combo bar decrease at a certain rate that pauses based on attack hit quality
    //  - Implement combo multiplier increase based on attack hit quality
    //  - Implement combo multiplier decrease based on time and attack hit quality (maybe just fully reset combo bar if they miss)

    //-----------------------------------------
    // Combo Variables
    //-----------------------------------------
    [SerializeField] private InputTranslator _inputTranslator;

    private float _comboMeterDrainRate = 2.5f;
    private float _comboDrainDelay;
    private float _comboMeterAmount = 0;
    private float _comboBaseIncrease = 10f;
    private float _comboGoodRate = 1.2f;
    private float _comboExcellentRate = 2f;
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

    private void Start()
    {
        _comboDrainDelay = TempoConductor.Instance.TimeBetweenBeats * 2f;
    }

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
            case TempoConductor.HitQuality.Miss:
                //Should we reset the combo meter on a miss, or decrease the combo meter by a certain amount?
                _comboMeterAmount = 0f;
                _isDraining = false;
                break;
        }

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
        _isDraining = false;
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
