using UnityEngine;

public class PlayerRef : MonoBehaviour
{
    private static PlayerRef _instance;
    public static PlayerRef Instance { get { return _instance; } }

    //Transform References
    private static Transform _playerTransform;
    public static Transform PlayerTransform { get { return _playerTransform; } }


    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        _playerTransform = transform;
    }
}
