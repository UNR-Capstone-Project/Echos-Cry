using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnGameStartEvent;
    public static void GameStart()
    {
        OnGameStartEvent?.Invoke();
    }
}
