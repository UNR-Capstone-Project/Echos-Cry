using UnityEngine;

public class Destructible
{
    [SerializeField] private GameObject destroyedVersion;

    protected void OnTriggerEnter(Collider collision)
    {
        if (collision.TryGetComponent<AttackCollisionHandler>(out AttackCollisionHandler handler))
        {
            //Instantiate(destroyedVersion, transform.position, transform.rotation);
            //Execute();
            //Destroy(gameObject);
        }
    }

    public void Start()
    {
        //Override enemy behaviors
    }
    public void OnDestroy()
    {
        //Override enemy behaviors
    }
}
