using AudioSystem;
using UnityEngine;

public class DroppedXp : MonoBehaviour
{
    [SerializeField] private float itemSpeed = 4f;
    [SerializeField] float itemDragDistance = 4f;
    [SerializeField] Rigidbody itemBody;
    [SerializeField] soundEffect pickupSFX;
    [SerializeField] Collider _collider;

    private int _xpValue = 10; //ISSUE, obviously

    private void Start()
    {
        TickManager.Instance.GetTimer(0.1f).Tick += MoveItemToPlayer; //0.1 felt more responsive than 0.2f
    }
    private void OnDestroy()
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
        //If not performant, store reference to player currency system
        if (other.TryGetComponent<Player>(out Player player))
        {
            player.XpSystem.IncrementXp(_xpValue);
        }
        SoundEffectManager.Instance.Builder
            .SetSound(pickupSFX)
            .SetSoundPosition(this.transform.position)
            .ValidateAndPlaySound();
        _collider.enabled = false;
        Destroy(gameObject);
    }
}
