using SoundSystem;
using UnityEngine;

public class MusicTester : MonoBehaviour
{
    [SerializeField] private MusicEvent testSong;

    void Start()
    {
        MusicManager.Instance.PlaySong(testSong);
    }
}
