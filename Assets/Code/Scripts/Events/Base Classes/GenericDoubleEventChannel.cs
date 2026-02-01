using System;
using UnityEngine;

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