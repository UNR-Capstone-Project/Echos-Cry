using System.Collections.Generic;
using UnityEngine;
using SoundSystem;
using System;
/// Original Author: Victor
/// All Contributors Since Creation: Victor, Michael, Andy

[CreateAssetMenu(fileName = "MusicManager", menuName = "Echo's Cry/Music System/Music Manager")]
public class MusicManager : ScriptableObject
{
    private static MusicManager _instance;
    public static MusicManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<MusicManager>("MusicManager");

                if (_instance == null)
                { //Scriptable object was not found in Resources!
                    _instance = CreateInstance<MusicManager>();
                }
                _instance.Initialize();
            }
            return _instance;
        }
    }

    public const int MAX_LAYER_COUNT = 5;
    private GameObject musicPlayerPrefab;

    [SerializeField] private MusicPlayer currentMusicPlayer;
    [SerializeField] private List<MusicPlayer> inactiveMusicPlayers = new List<MusicPlayer>();

    public event Action SongPlayEvent;
    public event Action SongStopEvent;

    private void Initialize()
    {
        musicPlayerPrefab = Resources.Load<GameObject>("MusicPlayer");
        if (musicPlayerPrefab == null) { Debug.Log("Music player was not found in resources!"); }
    }

    public float GetSampleProgress()
    {
        if (currentMusicPlayer != null)
        {
            return currentMusicPlayer.SampleProgress;
        }
        else return -1;
    }

    public float GetTempo()
    {
        if (currentMusicPlayer != null)
        {
            return currentMusicPlayer.bpm;
        }
        else return -1;
    }

    public MusicPlayer GetMusicPlayer()
    {
        if (currentMusicPlayer != null) return currentMusicPlayer;
        else return null;
    }

    public void PlaySong(MusicEvent music)
    {
        if (music == null) { return; }

        MusicPlayer newMusicPlayer = null;

        if (inactiveMusicPlayers.Count > 0) //Objects available in pool to use first
        {
            newMusicPlayer = inactiveMusicPlayers[0];
            inactiveMusicPlayers.RemoveAt(0);
            newMusicPlayer.gameObject.SetActive(true);
        }
        else
        {
            GameObject musicPlayerInstance = Instantiate(musicPlayerPrefab);
            newMusicPlayer = musicPlayerInstance.GetComponent<MusicPlayer>();
        }

        if (music.KeepTrackOfBeat == false)
        {
            newMusicPlayer.DisableBeatTracking();
        }

        newMusicPlayer.bpm = music.BPM;
        newMusicPlayer.SetupSong(music);
        newMusicPlayer.Play();

        currentMusicPlayer = newMusicPlayer;
        SongPlayEvent?.Invoke();
    }

    public void StopSong(MusicEvent music)
    {
        if (currentMusicPlayer != null) 
        { 
            currentMusicPlayer.Stop();
            currentMusicPlayer.gameObject.SetActive(false);
            inactiveMusicPlayers.Add(currentMusicPlayer);
            currentMusicPlayer = null;
            SongStopEvent?.Invoke();
        }
    }

    public void PauseSong()
    {
        if (currentMusicPlayer != null) 
        { 
            currentMusicPlayer.Pause();
            SongStopEvent?.Invoke();
        }
    }

    public void ResumeSong()
    {
        if (currentMusicPlayer != null) 
        {
            currentMusicPlayer.Resume(); 
            SongPlayEvent?.Invoke();
        }
    }

    public bool SongCurrentlyPlaying()
    {
        if (currentMusicPlayer != null)
        {
            if (currentMusicPlayer.SelfIsPlaying())
            {
                return true;
            }
        }
        return false;
    }
}
