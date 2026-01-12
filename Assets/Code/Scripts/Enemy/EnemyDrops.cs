using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    public void HandleEnemyDrops()
    {
        foreach (var itemDrop in ItemDrops)
        {
            int itemCount = Random.Range(itemDrop.minDropAmount, itemDrop.maxDropAmount);
            for (int i = 0; i < itemCount; i++)
            {
                GameObject itemInstance = Instantiate(itemDrop.prefab, transform.position, Quaternion.identity);
                Rigidbody itemRB = itemInstance.GetComponent<Rigidbody>();
                if (itemRB != null)
                {
                    Vector3 randDirection = Random.onUnitSphere;
                    randDirection.y = 0;
                    itemRB.AddForce(randDirection * itemExplosionSpeed, ForceMode.Impulse);
                }
            }
        }
    }

    public virtual void Start()
    {
        GetComponent<EnemyStats>().OnEnemyDeathEvent += HandleEnemyDrops;
    }
    public virtual void OnDestroy()
    {
        GetComponent<EnemyStats>().OnEnemyDeathEvent -= HandleEnemyDrops;
    }

    [SerializeField] private ItemDrop[] ItemDrops;
    [SerializeField] private float itemExplosionSpeed = 10f;
}
