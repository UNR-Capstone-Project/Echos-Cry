using UnityEngine;

public class HandleProjectileCollision : MonoBehaviour
{
    public float Timer = 5;
    private void Update()
    {
        Timer -= Time.deltaTime;
        if( Timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
