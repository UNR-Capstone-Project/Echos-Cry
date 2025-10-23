using UnityEngine;
using UnityEngine.Pool;

public class soundEffectManager : MonoBehaviour
{
    public const int DEFAULT_POOL_CAPACITY = 15;
    public const int MAX_SFX_PLAYERS = 30;
    public const int MAX_POOL_SIZE = 50;

    IObjectPool<soundEffectPlayer> sfxPlayersPool;

}
