using System;
using System.Collections.Generic;
using UnityEngine;

public class TimerNode
{
    private float _timer;
    private readonly float _tickTime;
    public event Action Tick;

    public TimerNode(float tickTime)
    {
        _timer = 0;
        _tickTime = tickTime;
    }
    public void Update()
    {
        _timer += Time.deltaTime;
        if(_timer >= _tickTime)
        {
            Tick?.Invoke();
            _timer -= _tickTime;
        }
    }
}
//Other TODO: may or may not be relevant to this script but design way to separate out tick events across frames, specifically for enemies (could use queue?)
public class TickManager : Singleton<TickManager>
{
    private Dictionary<float, TimerNode> _timers;

    private TimerNode AddTimer(float tickTime)
    {
        TimerNode newTimer = new(tickTime);
        _timers.Add(tickTime, newTimer);
        return newTimer;
    }
    public TimerNode GetTimer(float key)
    {
        if(_timers.ContainsKey(key)) return _timers[key]; 
        return AddTimer(key);
    }
    private void UpdateTimers()
    {
        //Michael:
        //Bug with this fuck ass thing. TODO: will change from dictionary system
        foreach(var timer in _timers)
        {
            timer.Value.Update();
        }
    }

    protected override void OnAwake()
    {
        _timers = new();
    }
    void Update()
    {
        UpdateTimers();
    }
    private void OnDisable()
    {
        _timers.Clear();
    }
}