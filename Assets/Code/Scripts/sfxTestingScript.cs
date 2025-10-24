using AudioSystem;
using UnityEngine;

public class sfxTestingScript : MonoBehaviour
{
    [SerializeField] soundEffect testSoundA;
    private Vector3 posA = Vector3.zero;

    void Start()
    {
       soundEffectManager.Instance.createSound()
                .setSound(testSoundA)
                .setSoundPosition(posA)
                .ValidateAndPlaySound();
    }

    
}
