using System;
using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// Singleton to manage all the sound effects in the game
/// </summary>
public class soundEffectManager : MonoBehaviour
{
    public static soundEffectManager Instance { get; private set; }
    public soundBuilder Builder { get; private set; }

    [SerializeField, HideInInspector] private soundEffectPlayer sfxPlayerPrefab;
    private bool collectionCheck = true;
    private int DEFAULT_POOL_CAPACITY = 30;
    private int MAX_SFX_PLAYERS = 30;
    private int MAX_POOL_SIZE = 50;
    IObjectPool<soundEffectPlayer> sfxPlayersPool;
    private Queue<soundEffectPlayer> frequentSfxPlayers = new();

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

        if (Application.isPlaying) initializeSoundPool();

        Builder = new soundBuilder();
        Builder.Initialize(this);
    }

    public bool canPlaySound(soundEffect sound)
    {
        if (sound == null) return false;
        if (!sound.isFrequent) return true;

        if (frequentSfxPlayers.Count >= MAX_SFX_PLAYERS && frequentSfxPlayers.TryDequeue(out var oldest))
        {
            oldest.Stop();
            releasePlayer(oldest);
        }

        return true;
    }

    //public soundBuilder createSound()
    //{
    //    if (Builder == null)
    //    {
    //        //Builder = ScriptableObject.CreateInstance<soundBuilder>();
    //        //Builder.Initialize(this);
    //    }
    //    return Builder;
    //}

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
        if (sfxPlayerPrefab == null)
        {
            GameObject temporary = new GameObject("SFXPlayer");
            return temporary.AddComponent<soundEffectPlayer>();
        }
        var soundPlayer = Instantiate(sfxPlayerPrefab);
        soundPlayer.gameObject.SetActive(false);
        return soundPlayer;
    }

    public soundEffectPlayer getPlayer()
    {
        if (sfxPlayersPool == null)
        {
            initializeSoundPool();
        }

        return sfxPlayersPool.Get();
    }

    public void releasePlayer(soundEffectPlayer player)
    {
        if (!player.gameObject.activeSelf) return;
        sfxPlayersPool.Release(player);
    }
    
    public void registerFrequentPlayer(soundEffectPlayer player)
    {
        frequentSfxPlayers.Enqueue(player);

        if (frequentSfxPlayers.Count > MAX_SFX_PLAYERS)
        {
            if (frequentSfxPlayers.TryDequeue(out var oldestPlayer))
            {
                oldestPlayer.Stop();
                sfxPlayersPool.Release(oldestPlayer);        
            }
        
        }
    }
    public void unregisterFrequentPlayer(soundEffectPlayer player)
    {
        var newQueue = new Queue<soundEffectPlayer>();
        while (frequentSfxPlayers.Count > 0)
        {
            var p = frequentSfxPlayers.Dequeue();
            if (p != player)
                newQueue.Enqueue(p);
        }
        frequentSfxPlayers = newQueue;
    }
}
