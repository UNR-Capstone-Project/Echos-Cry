using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnGameStartEvent;
    public static event Action OnPlayerDeathEvent;
    public static void GameStart()
    {
        OnGameStartEvent?.Invoke();
    }
    //Player parameter may not be used idk yet
    public static void HandlePlayerDeath(Player player)
    {
        OnPlayerDeathEvent?.Invoke();
    }
}
