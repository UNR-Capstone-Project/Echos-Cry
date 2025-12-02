using UnityEngine;

public abstract class EnemyBaseAttack : MonoBehaviour
{
    protected SimpleEnemyManager _enemyManager;
    protected virtual void Awake()
    {
        _enemyManager = GetComponent<SimpleEnemyManager>();
    }
    public abstract void UseAttack();
}
