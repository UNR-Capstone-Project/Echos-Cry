//using SoundSystem;
//using System.Collections;
//using System.Runtime.InteropServices.WindowsRuntime;
//using UnityEngine;
///// Original Author: Victor
///// All Contributors Since Creation: Victor
///// Last Modified By:
//public class MusicTester : MonoBehaviour
//{
//    [SerializeField] private MusicEvent testSong;

//    void Start()
//    {
//        StartCoroutine(WaitToPlaySong(1f));
//    }

//    IEnumerator WaitToPlaySong(float waitTime)
//    {
//        yield return new WaitForSeconds(waitTime);
//        MusicManager.Instance.PlaySong(testSong);
//    }
//}
