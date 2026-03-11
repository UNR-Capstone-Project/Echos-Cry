using UnityEngine;

[CreateAssetMenu(menuName = "Echo's Cry/Strategies/Drops/Enemy")]
public class EnemyDrops : ItemDropStrategy
{
    [SerializeField] private float itemExplosionSpeed = 10f;
    public override void Execute(Transform origin)
    {
        {
            foreach (var itemDrop in ItemDrops)
            {
                int itemCount = Random.Range(itemDrop.minDropAmount, itemDrop.maxDropAmount);
                for (int i = 0; i < itemCount; i++)
                {
                    GameObject itemInstance = Instantiate(itemDrop.prefab, origin.position, Quaternion.identity);
                    Rigidbody itemRB = itemInstance.GetComponent<Rigidbody>();
                    if (itemRB != null)
                    {
                        Vector3 randDirection = Random.onUnitSphere;
                        randDirection.y = 0;
                        itemRB.AddForce(randDirection * itemExplosionSpeed, ForceMode.Impulse);
                    }
                }
            }

            foreach (var itemChanceDrop in ItemChanceDrops)
            {
                int itemCount = itemChanceDrop.dropAmount;
                float chance = Random.Range(0.0f, 1.0f);

                if (chance > itemChanceDrop.dropChancePercent) continue;

                for (int i = 0; i < itemCount; i++)
                {
                    GameObject itemInstance = Instantiate(itemChanceDrop.prefab, origin.position, Quaternion.identity);
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
    }
}
