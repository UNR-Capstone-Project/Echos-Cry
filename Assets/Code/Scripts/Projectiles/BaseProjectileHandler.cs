using UnityEngine;

public abstract class BaseProjectileHandler : MonoBehaviour
{
    public abstract void UseProjectile(Vector3 position, Vector3 direction);
}
