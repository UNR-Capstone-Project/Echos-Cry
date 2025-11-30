using SoundSystem;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MusicTester : MonoBehaviour
{
    [SerializeField] private MusicEvent testSong;

    void Start()
    {
        StartCoroutine(WaitToPlaySong(2f));
    }

    IEnumerator WaitToPlaySong(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        MusicManager.Instance.PlaySong(testSong);
    }
}
