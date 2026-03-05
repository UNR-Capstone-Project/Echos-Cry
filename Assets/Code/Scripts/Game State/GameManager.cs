using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public static event Action OnGameStartEvent;
    public static event Action OnGameOverEvent;
    public static event Action OnPlayerDeathEvent;

    private static int _maxPlayerLives = 3;
    public static int PlayerLives = _maxPlayerLives;

    public static void GameStart()
    {
        OnGameStartEvent?.Invoke();
    }
    //Player parameter may not be used idk yet
    public static void HandlePlayerDeath(Player player)
    {
        PlayerLives--;
        OnPlayerDeathEvent?.Invoke();

        if (PlayerLives <= 0)
        {
            player.FullReset();
            PlayerLives = _maxPlayerLives;
            OnGameOverEvent?.Invoke();
        }
    }
}
