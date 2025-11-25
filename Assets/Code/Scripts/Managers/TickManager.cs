using System;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public static event Action OnTick02Event;
    public static event Action OnTick05Event;
    private float tick_timer02 = 0;
    private float tick_timer05 = 0;

    private void Start()
    {
        tick_timer02 = 0;
        tick_timer05 = 0;
    }
    void Update()
    {
        tick_timer02 += Time.deltaTime;
        tick_timer05 += Time.deltaTime;

        if (tick_timer02 >= 0.2f)
        {
            tick_timer02 = 0;
            OnTick02Event?.Invoke();
        }
        if (tick_timer05 >= 0.5f)
        {
            tick_timer05 = 0;
            OnTick05Event?.Invoke();
        }
    }
}
