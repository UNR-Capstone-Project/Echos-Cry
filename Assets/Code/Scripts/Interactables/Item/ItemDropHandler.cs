using AudioSystem;
using UnityEngine;

public abstract class ItemDropHandler : MonoBehaviour
{
    [SerializeField] protected float itemSpeed = 4f;
    [SerializeField] protected float itemDragDistance = 4f;
    [SerializeField] protected Rigidbody itemBody;
    [SerializeField] protected soundEffect pickupSFX;
    [SerializeField] protected Collider _collider;

    private void OnEnable()
    {
        TickManager.Instance.GetTimer(0.1f).Tick += MoveItemToPlayer; //0.1 felt more responsive than 0.2f
    }
    private void OnDisable()
    {
        if (TickManager.Instance != null) TickManager.Instance.GetTimer(0.1f).Tick -= MoveItemToPlayer;
    }
    private void MoveItemToPlayer()
    {
        Vector3 direction = (PlayerRef.Transform.position - transform.position);
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
        if(pickupSFX != null)
        {
            SoundEffectManager.Instance.Builder
                .SetSound(pickupSFX)
                .SetSoundPosition(transform.position)
                .ValidateAndPlaySound();
        }
        OnInteraction(other);
        _collider.enabled = false;
        Destroy(gameObject);
    }

    protected virtual void OnInteraction(Collider other) { }
}
