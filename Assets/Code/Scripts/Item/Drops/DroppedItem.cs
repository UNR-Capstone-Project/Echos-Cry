using AudioSystem;
using System;
using UnityEditor.UI;
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private float itemSpeed = 4f;
    [SerializeField] float itemDragDistance = 4f;
    [SerializeField] Rigidbody itemBody;
    [SerializeField] soundEffect pickupSFX;

    private void Start()
    {
        TickManager.OnTick01Event += MoveItemToPlayer;
    }
    private void OnDestroy()
    {
        TickManager.OnTick01Event -= MoveItemToPlayer;
    }

    private void MoveItemToPlayer()
    {
        float playerDistance = (gameObject.transform.position - PlayerRef.PlayerTransform.position).magnitude;
        if (playerDistance < itemDragDistance)
        {
            if (itemBody != null)
            {
                Vector3 direction = (PlayerRef.PlayerTransform.position - this.transform.position).normalized;
                itemBody.MovePosition(transform.position + direction * itemSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        soundEffectManager.Instance.Builder
            .setSound(pickupSFX)
            .setSoundPosition(this.transform.position)
            .ValidateAndPlaySound();
        PlayerStats.UpdateCurrency(1);
        GetComponent<Collider>().enabled = false;   
        Destroy(gameObject);
    
    }
}
