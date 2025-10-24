using UnityEngine;

//This will be placed on every enemy to handle collision with an attack from player

public class HandleDamageCollision : MonoBehaviour
{
    //TODO: Need way to grab whatever current damage player is doing
    private EnemyInfo _enemyInfo;
    private LayerMask _mask;

    private void Awake()
    {
        _mask = LayerMask.GetMask("PlayerAttack");
        _enemyInfo = GetComponent<EnemyInfo>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _mask) DamageHandler.Instance.AddToDamageQueue(_enemyInfo.EnemyID, 0f);
    }
}
