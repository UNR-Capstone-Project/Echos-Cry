using System;
using UnityEngine;
using UnityEngine.Pool;

public class soundEffectManager : MonoBehaviour
{
    [SerializeField] private soundEffectPlayer sfxPlayerPrefab;
    private bool collectionCheck = true;
    public const int DEFAULT_POOL_CAPACITY = 15;
    public const int MAX_SFX_PLAYERS = 30;
    public const int MAX_POOL_SIZE = 50;
    IObjectPool<soundEffectPlayer> sfxPlayersPool;

    private void Start()
    {
        initializeSoundPool();
    }

    public soundEffectPlayer getSoundPlayer()
    {
        return sfxPlayersPool.Get();
    }
    
    public void initializeSoundPool()
    {
        sfxPlayersPool = new ObjectPool<soundEffectPlayer>(
            createFunc: createSoundPlayer,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck,
            DEFAULT_POOL_CAPACITY,
            MAX_POOL_SIZE
        );
    }

    public void OnDestroyPoolObject(soundEffectPlayer player)
    {
        Destroy(player);
    }

    public void OnReturnedToPool(soundEffectPlayer player)
    {
        player.gameObject.SetActive(false);
    }

    public void OnTakeFromPool(soundEffectPlayer player)
    {
        player.gameObject.SetActive(true);
    }

    public soundEffectPlayer createSoundPlayer()
    {
        var soundPlayer = Instantiate(sfxPlayerPrefab);
        soundPlayer.gameObject.SetActive(false);
        return soundPlayer;
    }
}
