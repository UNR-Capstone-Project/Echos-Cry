using UnityEngine;

public class timedDestroy : MonoBehaviour
{
    public void setDestroy(float destroyTime)
    {
        Destroy(gameObject, destroyTime);
    }
}
