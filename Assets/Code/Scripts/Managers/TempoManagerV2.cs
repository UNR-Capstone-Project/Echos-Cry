using System;
using System.Collections;
using UnityEngine;

public class TempoManagerV2 : MonoBehaviour
{
    public enum HIT_QUALITY
    {
        MISS,
        BAD,
        GOOD,
        EXCELLENT
    }

    public void SetTempo(float new_tempo)
    {
        _tempo = new_tempo;

        //The time between each beat(60 seconds / BPM)
        _timeBetweenBeats = 60 / _tempo; //Seconds per beat

        _excellentHitTimeStart = _timeBetweenBeats * _excellentPercent;
        _goodHitTimeStart = _timeBetweenBeats * _goodPercent;
        _badHitTimeStart = _timeBetweenBeats * _badPercent;

        _excellentHitTimeEnd = _timeBetweenBeats - _excellentHitTimeStart;
        _goodHitTimeEnd = _timeBetweenBeats - _goodHitTimeStart;
        _badHitTimeEnd = _timeBetweenBeats - _badHitTimeStart;

        UpdateTempoEvent?.Invoke(_timeBetweenBeats);
    }

    private void BeatTick()
    {
        //_tickSound.Play();
        BeatTickEvent?.Invoke();
    }

    public void UpdateHitQuality()
    {
        if (_currentBeatTime < _excellentHitTimeStart || _currentBeatTime > _excellentHitTimeEnd) { currentHitQuality = HIT_QUALITY.EXCELLENT; }
        else if (_currentBeatTime < _goodHitTimeStart || _currentBeatTime > _goodHitTimeEnd) { currentHitQuality = HIT_QUALITY.GOOD; }
        else if (_currentBeatTime < _badHitTimeStart || _currentBeatTime > _badHitTimeEnd) { currentHitQuality = HIT_QUALITY.BAD; }
        else currentHitQuality = HIT_QUALITY.MISS;

        UpdateHitQualityEvent?.Invoke(currentHitQuality);
    }

    private void Awake()
    {
        _tickSound = GetComponent<AudioSource>();
    }

    void Start()
    {
        SetTempo(_tempo);
    }

    void Update()
    {
        _currentBeatTime += Time.deltaTime;
        while (_currentBeatTime > _timeBetweenBeats)
        {
            _currentBeatTime -= _timeBetweenBeats;
            BeatTick();
        }
    }

    //Tempo/Beat Values
    [SerializeField] private float _tempo = 85f;
    private float _timeBetweenBeats = 0;
    private float _currentBeatTime = 0;

    public HIT_QUALITY currentHitQuality = HIT_QUALITY.MISS;

    //Hit Time
    private float _excellentPercent = 0.07f;
    private float _goodPercent = 0.15f;
    private float _badPercent = 0.30f;

    //            Tempo Threshold
    // Start                           End
    //   |--|-|-|---------------|-|-|---|
    // BEAT 1                         BEAT 2

    //These HitTime measurements are used to compare the _currentBeatTime with whether it lands within the thresholds of Excellent, Good, Bad or Miss

    //   ***HitTimeStart represents the time value that is compared to the currentBeatTime at the start of the tempo threshold
    //   ***HitTimeEnd represents the time value that is compared to the currentBeatTime at the end of the tempo threshold

    private float _excellentHitTimeStart = 0;
    private float _goodHitTimeStart = 0;
    private float _badHitTimeStart = 0;
    private float _excellentHitTimeEnd = 0;
    private float _goodHitTimeEnd = 0;
    private float _badHitTimeEnd = 0;
    
    //Audio
    private AudioSource _tickSound;

    // Events
    public event Action BeatTickEvent;
    public event Action<float> UpdateTempoEvent;
    public event Action<HIT_QUALITY> UpdateHitQualityEvent;
}
