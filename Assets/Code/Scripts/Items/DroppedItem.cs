using AudioSystem;
using System;
using UnityEditor.UI;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    private GameObject mPlayer;
    [SerializeField] private float itemSpeed = 4f;
    [SerializeField] float itemDragDistance = 4f;
    [SerializeField] Rigidbody itemBody;
    [SerializeField] soundEffect pickupSFX;
    private bool itemDestroyed = false;

    private void Start()
    {
        mPlayer = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        //TODO: limit number of times check is performed to reduce calculations.
        if (mPlayer != null)
        {
            float playerDistance = (gameObject.transform.position - mPlayer.transform.position).magnitude;
            if (playerDistance < itemDragDistance)
            {
                if (itemBody != null)
                {
                    Vector3 direction = (mPlayer.transform.position - this.transform.position).normalized;
                    itemBody.MovePosition(transform.position + direction * itemSpeed * Time.deltaTime);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (itemDestroyed == false) //Stops item from being able to be picked up twice!
            {
                //Add finger count here!
                soundEffectManager.Instance.createSound()
                    .setSound(pickupSFX)
                    .setSoundPosition(this.transform.position)
                    .ValidateAndPlaySound();
                itemDestroyed = true;
                Destroy(gameObject);
            }
        }
    }
}
