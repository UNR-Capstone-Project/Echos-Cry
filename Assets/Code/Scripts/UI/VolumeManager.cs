using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; }
    [SerializeField] private Volume postProcessingVolume;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetDepthOfField(bool toggleState)
    {
        if (postProcessingVolume.profile.TryGet(out DepthOfField depthOfField))
        {
            depthOfField.active = toggleState;
        }
    }
}
