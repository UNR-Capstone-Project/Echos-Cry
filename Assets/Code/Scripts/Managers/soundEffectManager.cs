using System;
using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using UnityEngine.Pool;

public class soundEffectManager : MonoBehaviour
{
    public static soundEffectManager Instance { get; private set; }
    private soundBuilder builder;
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
    }

    public bool canPlaySound(soundEffect sound)
    {
        if (sound == null) return false;
        if (!sound.isFrequent) return true;

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

    public soundBuilder createSound()
    {
        if (builder == null)
        {
            builder = ScriptableObject.CreateInstance<soundBuilder>();
            builder.Initialize(this);
        }
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

}
