using SoundSystem;
using System.Collections;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public class SceneMusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicEvent song;

    void Start()
    {
        StartCoroutine(WaitToPlaySong(1f));
    }

    IEnumerator WaitToPlaySong(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        MusicManager.Instance.PlaySong(song);
    }
}
