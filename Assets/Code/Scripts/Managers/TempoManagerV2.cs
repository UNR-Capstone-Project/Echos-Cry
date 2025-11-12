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

    public void UpdateTempo()
    {
        //The time between each beat(60 seconds / BPM)
        _timeBetweenBeats = 60f / (float)MusicManager.Instance.GetTempo();

        _excellentHitTime = _timeBetweenBeats * _excellentPercent;
        _goodHitTime = _timeBetweenBeats * _goodPercent;
        _badHitTime = _timeBetweenBeats * _badPercent;
    }

    public HIT_QUALITY UpdateHitQuality()
    {
        if (_currentBeatTime > _timeBetweenBeats - _excellentHitTime || _currentBeatTime < _excellentHitTime) { currentHitQuality = HIT_QUALITY.EXCELLENT; }
        else if (_currentBeatTime > _timeBetweenBeats - _goodHitTime || _currentBeatTime < _goodHitTime) { currentHitQuality = HIT_QUALITY.GOOD; }
        else if (_currentBeatTime > _timeBetweenBeats - _badHitTime || _currentBeatTime < _badHitTime) { currentHitQuality = HIT_QUALITY.BAD; }
        else currentHitQuality = HIT_QUALITY.MISS;

        UpdateHitQualityEvent?.Invoke(currentHitQuality);
        return currentHitQuality;
    }

    void Start()
    {
        MusicManager.Instance.UpdateMusicPlayer += UpdateTempo;
    }

    void Update()
    {
        _currentBeatTime = MusicManager.Instance.GetSampleTime();

        if (_currentBeatTime < _lastBeatTime)
        {
            BeatTickEvent?.Invoke(); //Less precise beat tick than from music manager
        }
        _lastBeatTime = _currentBeatTime;
    }

    //Beat Values
    private float _timeBetweenBeats = 0;
    private float _currentBeatTime = 0;
    private float _lastBeatTime = 0f;

    public HIT_QUALITY currentHitQuality = HIT_QUALITY.MISS;

    //Hit Time
    private float _excellentPercent = 0.1f;
    private float _goodPercent = 0.15f;
    private float _badPercent = 0.25f;

    //            Tempo Threshold
    // Start                           End
    //   |--|-|-|---------------|-|-|---|
    // BEAT 1                         BEAT 2

    //These HitTime measurements are used to compare the _currentBeatTime with whether it lands within the thresholds of Excellent, Good, Bad or Miss

    //   ***HitTimeStart represents the time value that is compared to the currentBeatTime at the start of the tempo threshold
    //   ***HitTimeEnd represents the time value that is compared to the currentBeatTime at the end of the tempo threshold

    private float _excellentHitTime = 0;
    private float _goodHitTime = 0;
    private float _badHitTime= 0;

    // Events
    public event Action<HIT_QUALITY> UpdateHitQualityEvent;
    public event Action BeatTickEvent;
}
