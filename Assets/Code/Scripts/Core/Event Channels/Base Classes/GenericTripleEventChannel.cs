using System;
using UnityEngine;

public abstract class GenericTripleEventChannel<T, U, V> : ScriptableObject
{
    public event Action<T, U, V> Channel;

    public void Invoke(T parameter, U parameter2, V parameter3)
    {
        Channel?.Invoke(parameter, parameter2, parameter3);
    }
    private void OnDisable()
    {
        Channel = null;
    }
}