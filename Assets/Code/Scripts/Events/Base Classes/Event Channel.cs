using System;
using UnityEngine;

//Inspired and implemented from https://blog.thebear.dev/how-to-event-systems-in-unity and https://unity.com/how-to/scriptableobjects-event-channels-game-code

[CreateAssetMenu(menuName = "Echo's Cry/Events/Event(Void)")]
public class EventChannel : ScriptableObject
{
    public event Action Channel;

    public void Invoke()
    {
        Channel?.Invoke();
    }
    private void OnDisable()
    {
        Channel = null;
    }
}
