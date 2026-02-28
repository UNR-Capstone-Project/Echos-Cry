using UnityEngine;

public class timedDestroy : MonoBehaviour
{
    [SerializeField] private float destroyTime = 5f;
    [SerializeField] private bool destroyOnStart = false;
    private void Start()
    {
        if (destroyOnStart)
            setDestroy(destroyTime);
    }
    public void setDestroy(float destroyTime)
    {
        Destroy(gameObject, destroyTime);
    }
}
