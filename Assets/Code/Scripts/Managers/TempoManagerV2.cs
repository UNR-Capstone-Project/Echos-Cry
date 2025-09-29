using System.Collections;
using UnityEngine;

public class TempoManagerV2 : MonoBehaviour
{
    [SerializeField] private GameObject RenderMetronome;

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
        _timeBetweenBeats = 60 / _tempo;

        _excellentHitTimeStart = _timeBetweenBeats * _excellentPercent;
        _goodHitTimeStart = _timeBetweenBeats * _goodPercent;
        _badHitTimeStart = _timeBetweenBeats * _badPercent;

        _excellentHitTimeEnd = _timeBetweenBeats - _excellentHitTimeStart;
        _goodHitTimeEnd = _timeBetweenBeats - _goodHitTimeStart;
        _badHitTimeEnd = _timeBetweenBeats - _badHitTimeStart;

        RenderMetronome.GetComponent<RenderMetronomeManager>().SetPendulumSpeed(_tempo / 60f); //Syncs the metronomes pendulum swing animation with current tempo.
    }

    private void BeatTick()
    {
        GameObject.FindWithTag("MainCanvas").GetComponent<CanvasManager>().FlashOutline(0.1f);
        
        _tickSound.Play();
        _currentBeatTime = 0;
    }

    public HIT_QUALITY CheckHitQuality() //ISSUE: Naming inconsistencies! --- maybe _excellentHitTimeStart and _excellentHitTimeEnd are meant to be swapped?
    {
        HIT_QUALITY quality;
        if (_currentBeatTime < _excellentHitTimeStart || _currentBeatTime > _excellentHitTimeEnd) { quality = HIT_QUALITY.EXCELLENT; }
        else if (_currentBeatTime < _goodHitTimeStart || _currentBeatTime > _goodHitTimeEnd) { quality = HIT_QUALITY.GOOD; }
        else if (_currentBeatTime < _badHitTimeStart || _currentBeatTime > _badHitTimeEnd) { quality = HIT_QUALITY.BAD; }
        else { quality = HIT_QUALITY.MISS; }

        GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<CanvasManager>().UpdateHitQualityText(quality.ToString()); //Update canvas with hit quality text

        return quality;
    }

    public void StartBeatTick()
    {
        StartCoroutine(UpdateBeatTick());
    }

    public void StopBeatTick()
    {
        StopCoroutine(UpdateBeatTick());
    }

    private IEnumerator UpdateBeatTick()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeBetweenBeats);
            BeatTick();
        }
    }

    void Start()
    {
        _tickSound = GetComponent<AudioSource>();

        SetTempo(_tempo);
        StartBeatTick();
        //_timeToPlayTick = _timeBetweenBeats - (_tickSound.clip.length/2);
    }

    void Update()
    {
        _currentBeatTime += Time.deltaTime;
    }

    //Tempo/Beat Values
    [SerializeField] private float _tempo = 60f;
    private float _timeBetweenBeats = 0;
    private float _currentBeatTime = 0;

    //Hit Time Percentage
    private float _excellentPercent = 0.07f;
    private float _goodPercent = 0.15f;
    private float _badPercent = 0.30f;

    //    Start                     End
    //      |------------------------|
    //   BEAT 1                    BEAT 2
    //These HitTime measurements are used to compare the _currentBeatTime with whether it lands within the thresholds of Excellent, Good, Bad or Miss
    private float _excellentHitTimeStart = 0;
    private float _goodHitTimeStart = 0;
    private float _badHitTimeStart = 0;
    private float _excellentHitTimeEnd = 0;
    private float _goodHitTimeEnd = 0;
    private float _badHitTimeEnd = 0;
    
    //Audio
    private AudioSource _tickSound;
    private float _timeToPlayTick = 0;
}
