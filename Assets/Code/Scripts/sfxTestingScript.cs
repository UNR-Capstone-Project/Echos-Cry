using System;
using System.Collections;
using AudioSystem;
using UnityEngine;
using Random = UnityEngine.Random;
public class sfxTestingScript : MonoBehaviour
{
    [SerializeField] soundEffect singleSound;
    [SerializeField] soundEffect multiClipSound;
    private Vector3 posA = Vector3.zero;

    private IEnumerator Start()
    {
        
        for (int i = 0; i < 2; i++)
        {
            int randomSound;

            if (i == 0)
            {
                randomSound = 1;
            } else
            {
                randomSound = 2;
            }

            if (randomSound == 1)
            {
                soundEffectManager.Instance.createSound()
                    .setSound(singleSound)
                    .setSoundPosition(posA)
                    .ValidateAndPlaySound();
                Debug.Log("SOUND A PLAYED");
            }
            else
            {
                soundEffectManager.Instance.createSound()
                    .setSound(multiClipSound)
                    .setSoundPosition(posA)
                    .ValidateAndPlaySound();
                Debug.Log("====SECOND SOUND PLAYED====");
            }

            yield return new WaitForSeconds(3);
        }

        
    }

}
