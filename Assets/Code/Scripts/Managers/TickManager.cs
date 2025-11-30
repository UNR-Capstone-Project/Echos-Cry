using System;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public static event Action OnTick01Event;
    public static event Action OnTick02Event;
    public static event Action OnTick05Event;
    private float tick_timer01 = 0;
    private float tick_timer02 = 0;
    private float tick_timer05 = 0;

    private void Start()
    {
        tick_timer01 = 0;
        tick_timer02 = 0;
        tick_timer05 = 0;
    }
    void Update()
    {
        tick_timer01 += Time.deltaTime;
        tick_timer02 += Time.deltaTime;
        tick_timer05 += Time.deltaTime;
        if(tick_timer01 >= 0.1f)
        {
            tick_timer01 -= 0.1f;
            OnTick01Event?.Invoke();
        }
        if (tick_timer02 >= 0.2f)
        {
            tick_timer02 -= 0.2f;
            OnTick02Event?.Invoke();
        }
        if (tick_timer05 >= 0.5f)
        {
            tick_timer05 -= 0.5f;
            OnTick05Event?.Invoke();
        }
    }
}
