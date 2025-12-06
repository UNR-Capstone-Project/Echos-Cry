using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager Instance { get; private set; }
    [SerializeField] private Volume postProcessingVolume;
    private float defaultSaturation;
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

        //---Set Default Volume Values---
        if (postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            defaultSaturation = colorAdjustments.saturation.value;
        }
    }

    public void SetColorSaturationGrey()
    {
        if (postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.saturation.value = -100f;
        }
    }

    public void ResetColorSaturation()
    {
        if (postProcessingVolume.profile.TryGet(out ColorAdjustments colorAdjustments))
        {
            colorAdjustments.saturation.value = defaultSaturation;
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
