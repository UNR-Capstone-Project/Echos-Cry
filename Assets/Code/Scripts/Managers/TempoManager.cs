using System;
using System.Collections;
using UnityEngine;

public class TempoManager : MonoBehaviour
{
    public void UpdateTempo()
    {
        //The time between each beat(60 seconds / BPM)
        _timeBetweenBeats = 60f / (float)MusicManager.Instance.GetTempo();

        _excellentHitTime = _timeBetweenBeats * _excellentPercent;
        _goodHitTime = _timeBetweenBeats * _goodPercent;
        //_badHitTime = _timeBetweenBeats * _badPercent;
    }

    private static void UpdateHitQuality()
    {
        float distanceToStart = _currentBeatTime;                             // how far after beat start
        float distanceToEnd   = _timeBetweenBeats - _currentBeatTime;  
        float distance = Mathf.Min(distanceToStart, distanceToEnd);

        if (distance <= _excellentHitTime)
        {
            CurrentHitQuality = HIT_QUALITY.EXCELLENT;
        } else if (distance <= _goodHitTime)
        {
            CurrentHitQuality = HIT_QUALITY.GOOD;
        } else
        {
            CurrentHitQuality = HIT_QUALITY.MISS;
        } 
            
        /*
        //HIT_QUALITY currentHitQuality;
        if (_currentBeatTime > _timeBetweenBeats - _excellentHitTime || _currentBeatTime < _excellentHitTime) { CurrentHitQuality = HIT_QUALITY.EXCELLENT; }
        else if (_currentBeatTime > _timeBetweenBeats - _goodHitTime || _currentBeatTime < _goodHitTime) { CurrentHitQuality = HIT_QUALITY.GOOD; }
        //else if (_currentBeatTime > _timeBetweenBeats - _badHitTime || _currentBeatTime < _badHitTime) { CurrentHitQuality = HIT_QUALITY.BAD; }
        else CurrentHitQuality = HIT_QUALITY.MISS;
        */
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
    }
    void Start()
    {
        MusicManager.Instance.UpdateMusicPlayer += UpdateTempo;
    }
    private void OnDestroy()
    {
        MusicManager.Instance.UpdateMusicPlayer -= UpdateTempo;
        _instance = null;
    }

    void Update()
    {
        _currentBeatTime = MusicManager.Instance.GetSampleProgress() * _timeBetweenBeats;

        if (MusicManager.Instance.GetSampleProgress() < _lastProgress)
        {
            BeatTickEvent?.Invoke(); //Less precise beat tick than from music manager
        }
        _lastProgress = MusicManager.Instance.GetSampleProgress();
        //_lastBeatTime = _currentBeatTime;

        UpdateHitQuality();
    }

    public enum HIT_QUALITY
    {
        MISS = 0,
        //BAD,
        GOOD,
        EXCELLENT
    }

    private static TempoManager _instance;
    public static TempoManager  Instance {  get { return _instance; } }

    public static HIT_QUALITY CurrentHitQuality;

    //Beat Timing Values
    private static float _timeBetweenBeats = 0;
    public static float TimeBetweenBeats { get { return _timeBetweenBeats; } }
    private static float _currentBeatTime = 0;
    private float _lastProgress = 0f;

    //Hit Time
    private static float _excellentPercent = 0.08f; 
    private static float _goodPercent = 0.15f; 
    //private static float _lastBeatTime = 0f;
    /* ms timing windows calculation

    for 100 beats per minute

    60 seconds / 100 beats = 0.6 seconds * 1000 = 6000ms
    percentage calculations
    excellent 
    6000 * 0.05 = 30ms allowed maximum window

    good 
    6000 * 0.125 = 75ms allowed maximum window 
    */
    //private static float _badPercent = 0.25f;

    //            Tempo Threshold
    // Start                           End
    //   |--|-|-|---------------|-|-|---|
    // BEAT 1                         BEAT 2

    //These HitTime measurements are used to compare the _currentBeatTime with whether it lands within the thresholds of Excellent, Good, Bad or Miss

    //   ***HitTimeStart represents the time value that is compared to the currentBeatTime at the start of the tempo threshold
    //   ***HitTimeEnd represents the time value that is compared to the currentBeatTime at the end of the tempo threshold

    private static float _excellentHitTime = 0;
    private static float _goodHitTime = 0;
    //private static float _badHitTime= 0;

    // Events
    //public static event Action<HIT_QUALITY> UpdateHitQualityEvent;
    public static event Action BeatTickEvent;
}
