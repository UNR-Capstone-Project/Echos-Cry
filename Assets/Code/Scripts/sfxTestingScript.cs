using AudioSystem;
using UnityEngine;

public class sfxTestingScript : MonoBehaviour
{
    [SerializeField] soundEffect testSoundA;
    private Vector3 posA = Vector3.zero;

    [SerializeField] soundEffectPlayer playerMethodCheck;

    void Start()
    {
        //soundEffectManager.Instance.createSoundPlayer()
        
        //player can setup a sound and play and stop correctly
        playerMethodCheck.setupSoundEffect(testSoundA);
        playerMethodCheck.Play();
    }

    
}
