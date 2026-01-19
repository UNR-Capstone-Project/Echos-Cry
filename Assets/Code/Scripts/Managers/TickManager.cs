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

public class TickManager : MonoBehaviour
{
    private static TickManager _instance;
    public static TickManager Instance
    {
        get
        {
            if( _instance == null)
            {
                TickManager newInstance = new GameObject("Tick Manager").AddComponent<TickManager>();
                _instance = newInstance;
            }
            return _instance;
        }
    }

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
        foreach(var value in _timers.Values)
        {
            value.Update();
        }
    }

    private void Awake()
    {
        if( _instance != null )
        {
            Destroy(this);
            return;
        }
        _timers = new();
    }
    void Update()
    {
        UpdateTimers();
    }
}