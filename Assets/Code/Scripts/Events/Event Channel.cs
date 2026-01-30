using System;
using UnityEngine;

//Inspired from https://blog.thebear.dev/how-to-event-systems-in-unity and https://unity.com/how-to/scriptableobjects-event-channels-game-code

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

public abstract class GenericSingleEventChannel<T> : ScriptableObject
{
    public event Action<T> Channel;

    public void Invoke(T parameter)
    {
        Channel?.Invoke(parameter);
    }
    private void OnDisable()
    {
        Channel = null;
    }
}

public abstract class GenericDoubleEventChannel<T, U> : ScriptableObject
{
    public event Action<T, U> Channel;

    public void Invoke(T parameter, U parameter2)
    {
        Channel?.Invoke(parameter, parameter2);
    }
    private void OnDisable()
    {
        Channel = null;
    }
}
