using System;
using System.Collections;
using System.Collections.Generic;
using AudioSystem;
using UnityEngine;
using UnityEngine.Pool;
/// Original Author: Victor
/// All Contributors Since Creation: Victor, Michael
/// Last Modified By: Michael
/// 
/// <summary>
/// Singleton to manage all the sound effects in the game
/// </summary>
public class SoundEffectManager : Singleton<SoundEffectManager>
{
    public SoundBuilder Builder { get; private set; }

    [SerializeField, HideInInspector] private SoundEffectPlayer sfxPlayerPrefab;
    private bool collectionCheck = true;
    private int DEFAULT_POOL_CAPACITY = 30;
    private int MAX_SFX_PLAYERS = 30;
    private int MAX_POOL_SIZE = 50;
    IObjectPool<SoundEffectPlayer> sfxPlayersPool;
    private Queue<SoundEffectPlayer> frequentSfxPlayers = new();

    protected override void OnAwake()
    {
        if (Application.isPlaying) InitializeSoundPool();

        Builder = new SoundBuilder();
        Builder.Initialize(this);
    }

    public bool CanPlaySound(soundEffect sound)
    {
        if (sound == null) return false;
        if (!sound.isFrequent) return true;

        if (frequentSfxPlayers.Count >= MAX_SFX_PLAYERS && frequentSfxPlayers.TryDequeue(out var oldest))
        {
            oldest.Stop();
            ReleasePlayer(oldest);
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

    public void InitializeSoundPool()
    {
        sfxPlayersPool = new ObjectPool<SoundEffectPlayer>(
            createFunc: CreateSoundPlayer,
            actionOnGet: OnTakeFromPool,
            actionOnRelease: OnReturnedToPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck,
            DEFAULT_POOL_CAPACITY,
            MAX_POOL_SIZE
        );
    }

    public void OnDestroyPoolObject(SoundEffectPlayer player)
    {
        Destroy(player);
    }

    public void OnReturnedToPool(SoundEffectPlayer player)
    {
        player.gameObject.SetActive(false);
    }

    public void OnTakeFromPool(SoundEffectPlayer player)
    {
        player.gameObject.SetActive(true);
    }

    public SoundEffectPlayer CreateSoundPlayer()
    {
        if (sfxPlayerPrefab == null)
        {
            GameObject temporary = new GameObject("SFXPlayer");
            return temporary.AddComponent<SoundEffectPlayer>();
        }
        var soundPlayer = Instantiate(sfxPlayerPrefab);
        soundPlayer.gameObject.SetActive(false);
        return soundPlayer;
    }

    public SoundEffectPlayer GetPlayer()
    {
        if (sfxPlayersPool == null)
        {
            InitializeSoundPool();
        }

        return sfxPlayersPool.Get();
    }

    public void ReleasePlayer(SoundEffectPlayer player)
    {
        if (!player.gameObject.activeSelf) return;
        sfxPlayersPool.Release(player);
    }
    
    public void RegisterFrequentPlayer(SoundEffectPlayer player)
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
    public void UnregisterFrequentPlayer(SoundEffectPlayer player)
    {
        var newQueue = new Queue<SoundEffectPlayer>();
        while (frequentSfxPlayers.Count > 0)
        {
            var p = frequentSfxPlayers.Dequeue();
            if (p != player)
                newQueue.Enqueue(p);
        }
        frequentSfxPlayers = newQueue;
    }
}
