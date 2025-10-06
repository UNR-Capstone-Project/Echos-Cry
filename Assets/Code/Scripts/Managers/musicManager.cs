using System.Collections.Generic;
using UnityEngine;
using SoundSystem;
using UnityEditor.PackageManager;

[CreateAssetMenu(fileName = "MusicManager", menuName = "Scriptable Objects/Music Manager")]
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
            }

            _instance.Initialize();

            return _instance;
        }
    }

    public const int MAX_LAYER_COUNT = 5;
    private GameObject musicPlayerPrefab;
    //[SerializeField] private float globalMusicVolume;
    [SerializeField] private float crossfadeTime = 0.5f;

    [SerializeField] private MusicPlayer currentMusicPlayer;
    [SerializeField] private List<MusicPlayer> inactiveMusicPlayers = new List<MusicPlayer>();

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

    public void PlaySong(MusicEvent music)
    {
        if (music == null) { return; }

        MusicPlayer newMusicPlayer = null;

        if (inactiveMusicPlayers.Count > 0) //Objects available in pool to use first
        {
            newMusicPlayer = inactiveMusicPlayers[0];
            inactiveMusicPlayers.RemoveAt(0);
        }
        else
        {
            GameObject musicPlayerInstance = Instantiate(musicPlayerPrefab);
            newMusicPlayer = musicPlayerInstance.GetComponent<MusicPlayer>();
        }

        newMusicPlayer.SetupSong(music);
        newMusicPlayer.Play();
        //ISSUE: add function in player -> newMusicPlayer.PlayWithCrossfade(crossfadeTime);
        currentMusicPlayer = newMusicPlayer;
    }

    public void StopSong(MusicEvent music)
    {
        if (currentMusicPlayer != null) 
        { 
            currentMusicPlayer.Stop();
            inactiveMusicPlayers.Add(currentMusicPlayer);
            currentMusicPlayer = null;
        }
    }

    public void PauseSong()
    {
        if (currentMusicPlayer != null) { currentMusicPlayer.Pause(); }
    }

    public void ResumeSong()
    {
        if (currentMusicPlayer != null) { currentMusicPlayer.Resume(); }
    }
}
