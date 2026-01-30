using System;
using UnityEngine;

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
