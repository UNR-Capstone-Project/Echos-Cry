using System;
using AudioSystem;
using UnityEngine;

public class sfxTestingScript : MonoBehaviour
{
    [SerializeField] soundEffect singleSound;
    [SerializeField] soundEffect multiClipSound;
    private Vector3 posA = Vector3.zero;

    void Start()
    {
        soundEffectManager.Instance.createSound()
            .setSound(multiClipSound)
            .setSoundPosition(posA)
            .ValidateAndPlaySound();

        
    }

    
}
