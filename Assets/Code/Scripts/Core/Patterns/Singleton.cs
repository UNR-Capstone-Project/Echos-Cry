using System;
using UnityEngine;

//Implementation idea from https://gamedev.stackexchange.com/questions/116009/in-unity-how-do-i-correctly-implement-the-singleton-pattern
//A spawnable singleton that will simple instantiate the singleton if it doesn't already exist in scene
//Non spawnable version below
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool IsQuitting{  get; private set; }
    
    private static T _instance = null;
    public static T Instance 
    {  
        get 
        {
            if (IsQuitting) return null;

            if (_instance != null) return _instance;

            var instances = FindObjectsByType<T>(sortMode: FindObjectsSortMode.None);
            var count = instances.Length;
            if(count > 0)
            {
                if(count == 1) return _instance = instances[0];
                for(int i = 1; i < count; i++)
                {
                    Destroy(instances[i]);
                }
                return _instance = instances[0];
            }

            return _instance = new GameObject(typeof(T).Name.ToString()).AddComponent<T>();
        } 
    }

    private void OnApplicationQuit()
    {
        IsQuitting = true;
    }
    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        OnAwake();
    }
    protected virtual void OnAwake() { }
}

//Non spawnable version of Singleton above as not every singleton will want to be spawned when referenced
public abstract class NonSpawnableSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static bool IsQuitting { get; private set; }

    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (IsQuitting) return null;

            if (_instance != null) return _instance;

            else
            {
                var instances = FindObjectsByType<T>(sortMode: FindObjectsSortMode.None);
                var count = instances.Length;
                if (count > 0)
                {
                    if (count == 1) return _instance = instances[0];
                    for (int i = 1; i < count; i++)
                    {
                        Destroy(instances[i]);
                    }
                    return _instance = instances[0];
                }
                else return null;
            }
        }
    }
    private void OnApplicationQuit()
    {
        IsQuitting = true;
    }
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        OnAwake();
    }
    protected virtual void OnAwake() { }
}
