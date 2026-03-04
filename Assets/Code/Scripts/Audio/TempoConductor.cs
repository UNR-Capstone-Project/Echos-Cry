using System;
using System.Collections;
using UnityEngine;

public class TempoConductor : Singleton<TempoConductor>
{
    public enum HitQuality
    {
        Miss = 0,
        Good,
        Excellent
    }

    private HitQuality _currentHitQuality;
    public HitQuality CurrentHitQuality { get { return _currentHitQuality; } }

    //Beat Timing Values
    private float _currentBeatProgress = 0;
    private float _timeBetweenBeats;

    public float TimeBetweenBeats { get { return _timeBetweenBeats; } }

    //Hit Time --> Higher values are easier to hit within!
    private float _excellentPercent;
    private float _goodPercent;

    //            Tempo Threshold
    // Start                           End
    //   |--|-|-|---------------|-|-|---|
    // BEAT 1                         BEAT 2

    // Events
    public event Action BeatTickEvent;

    public bool IsOnBeat()
    {
        return (_currentHitQuality == HitQuality.Good || _currentHitQuality == HitQuality.Excellent);
    }
    void Update()
    {
        UpdateHitQuality();
    }

    private void UpdateTempo()
    {
        //The time between each beat: 60 seconds / BPM
        _timeBetweenBeats = 60f / (float)MusicManager.Instance.GetTempo();
    }

    private void UpdateHitQuality()
    {
        _currentBeatProgress = MusicManager.Instance.GetSampleProgress();
        //Debug.Log(_currentBeatProgress);

        if (_currentBeatProgress <= _excellentPercent ||
            _currentBeatProgress >= (1f - _excellentPercent))
        {
            _currentHitQuality = HitQuality.Excellent;
        }
        else if (_currentBeatProgress <= _goodPercent ||
            _currentBeatProgress >= (1f - _goodPercent))
        {
            _currentHitQuality = HitQuality.Good;
        }
        else
        {
            _currentHitQuality = HitQuality.Miss;
        }
    }

    private void Awake()
    {
        //Higher values are easier to hit within!
        //This works by creating values for every 10th percentile.
        //So if your accuracy is between 90% - 100%, then it is the hardest range of 0.1 for excellent and 0.2 for good.
        //The lowest accuracy of 0% - 10% creates a range of 0.3 for excellent and 0.4 for good.

        float accuracy = Mathf.Clamp01(CalibrationManager.HitAccuracy);

        float t = Mathf.InverseLerp(0.1f, 0.9f, accuracy);
        t = 1f - t;

        _excellentPercent = Mathf.Lerp(0.1f, 0.3f, t);
        _goodPercent = Mathf.Lerp(0.2f, 0.4f, t);

        _excellentPercent = Mathf.Clamp(_excellentPercent, 0f, 1f);
        _goodPercent = Mathf.Clamp(_goodPercent, 0f, 1f);

        Debug.Log($"Excellent:{_excellentPercent} Good:{_goodPercent}");
    }

    private void OnEnable()
    {
        MusicManager.Instance.SongPlayEvent += UpdateTempo;
        UpdateTempo();
    }
    private void OnDisable()
    {
        MusicManager.Instance.SongPlayEvent -= UpdateTempo;
    }
}