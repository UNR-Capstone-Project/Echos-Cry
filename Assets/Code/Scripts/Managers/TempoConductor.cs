using System;
using System.Collections;
using UnityEngine;

public class TempoConductor : MonoBehaviour
{
    public enum HitQuality
    {
        Miss = 0,
        Good,
        Excellent
    }

    private static TempoConductor _instance;
    public static TempoConductor  Instance {  
        get 
        { 
            if (_instance == null)
            {
                _instance = new GameObject("Tempo Manager").AddComponent<TempoConductor>();
            }
            return _instance; 
        } 
    }

    private HitQuality _currentHitQuality;
    public HitQuality CurrentHitQuality { get { return _currentHitQuality; } }

    //Beat Timing Values
    private float _timeBetweenBeats = 0;
    private float _currentBeatTime = 0;
    private float _lastProgress = 0f;
    
    public float TimeBetweenBeats { get { return _timeBetweenBeats; } }

    //Hit Time
    private readonly float _excellentPercent = 0.1f; 
    private readonly float _goodPercent = 0.2f; 

    //            Tempo Threshold
    // Start                           End
    //   |--|-|-|---------------|-|-|---|
    // BEAT 1                         BEAT 2

    //These HitTime measurements are used to compare the _currentBeatTime with whether it lands within the thresholds of Excellent, Good, Bad or Miss

    //   ***HitTimeStart represents the time value that is compared to the currentBeatTime at the start of the tempo threshold
    //   ***HitTimeEnd represents the time value that is compared to the currentBeatTime at the end of the tempo threshold

    private float _excellentHitTime = 0;
    private float _goodHitTime = 0;

    // Events
    public event Action BeatTickEvent;

    public bool IsOnBeat()
    {
        if (_currentHitQuality == HitQuality.Good || _currentHitQuality == HitQuality.Excellent) 
            return true;
        else
            return false;
    }
    private void UpdateTempo()
    {
        //The time between each beat(60 seconds / BPM)
        _timeBetweenBeats = 60f / (float)MusicManager.Instance.GetTempo();

        _excellentHitTime = _timeBetweenBeats * _excellentPercent;
        _goodHitTime = _timeBetweenBeats * _goodPercent;
    }
    private void UpdateHitQuality()
    {
        if (_currentBeatTime > _timeBetweenBeats - _excellentHitTime || _currentBeatTime < _excellentHitTime)  
            _currentHitQuality = HitQuality.Excellent; 
        else if (_currentBeatTime > _timeBetweenBeats - _goodHitTime || _currentBeatTime < _goodHitTime)  
            _currentHitQuality = HitQuality.Good; 
        else 
            _currentHitQuality = HitQuality.Miss;
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }
    }
    void Start()
    {
        MusicManager.Instance.SongPlayEvent += UpdateTempo;
        UpdateTempo();
    }
    private void OnDestroy()
    {
        MusicManager.Instance.SongPlayEvent -= UpdateTempo;
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

        UpdateHitQuality();
    }
}