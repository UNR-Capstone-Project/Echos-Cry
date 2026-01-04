using AudioSystem;
using System;
#if UNITY_EDITOR
using UnityEditor.UI;
#endif
using UnityEngine;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private float itemSpeed = 4f;
    [SerializeField] float itemDragDistance = 4f;
    [SerializeField] Rigidbody itemBody;
    [SerializeField] soundEffect pickupSFX;

    private void Awake()
    {
        itemBody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        TickManager.OnTick02Event += MoveItemToPlayer;
    }
    private void OnDestroy()
    {
        TickManager.OnTick02Event -= MoveItemToPlayer;
    }

    private void MoveItemToPlayer()
    {
        Vector3 direction = (PlayerRef.PlayerTransform.position - transform.position);
        direction.y = 0;
        float playerDistance = direction.magnitude;
        if (playerDistance < itemDragDistance)
        {
            if (itemBody != null)
            {
                direction.Normalize();
                itemBody.AddForce(direction * itemSpeed, ForceMode.Impulse);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        soundEffectManager.Instance.Builder
            .setSound(pickupSFX)
            .setSoundPosition(this.transform.position)
            .ValidateAndPlaySound();
        //PlayerStats.UpdateCurrency(1);
        GetComponent<Collider>().enabled = false;   
        Destroy(gameObject);
    }
}
