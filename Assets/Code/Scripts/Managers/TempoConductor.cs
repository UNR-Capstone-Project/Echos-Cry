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
    private readonly float _excellentPercent = 0.1f;
    private readonly float _goodPercent = 0.2f;

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

    void OnEnable()
    {
        MusicManager.Instance.SongPlayEvent += UpdateTempo;
        UpdateTempo();
    }
    private void OnDisable()
    {
        MusicManager.Instance.SongPlayEvent -= UpdateTempo;
    }
}