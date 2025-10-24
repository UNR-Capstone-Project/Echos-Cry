using System;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using UnityEngine.Pool;

public class soundEffectManager : MonoBehaviour
{
    public static soundEffectManager Instance { get; private set; }
    private soundBuilder builder;
    [SerializeField] private soundEffectPlayer sfxPlayerPrefab;
    [SerializeField] private bool collectionCheck = true;
    [SerializeField] int DEFAULT_POOL_CAPACITY = 15;
    [SerializeField] int MAX_SFX_PLAYERS = 30;
    [SerializeField] int MAX_POOL_SIZE = 50;
    IObjectPool<soundEffectPlayer> sfxPlayersPool;
    public readonly Queue<soundEffectPlayer> frequentSfxPlayers = new();

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        
        
    }

    private void Start()
    {
        //builder = ScriptableObject.CreateInstance<soundBuilder>();
        //builder.Initialize(this);
        //initializeSoundPool();
    }
    
    public bool canPlaySound(soundEffect sound)
    {
        if (!sound.frequentlyPlayed)
        {
            return true;
        }

        if (frequentSfxPlayers.Count > MAX_SFX_PLAYERS && frequentSfxPlayers.TryDequeue(out var player))
        {
            try
            {
                player.Stop();
                return true;
            }
            catch
            {
                Debug.Log("SFXPlayer already has been released");
            }
            return false;
        }
        return true;
    }

    public soundBuilder createSound() {
        /*
        if (builder == null)
        {
            builder = ScriptableObject.CreateInstance<soundBuilder>();
            builder.Initialize(this);
        }*/
        return builder;
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

    public soundEffectPlayer getPlayer()
    {
        return sfxPlayersPool.Get();
    }

}
