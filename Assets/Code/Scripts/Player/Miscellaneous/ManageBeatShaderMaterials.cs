using UnityEngine;

public class ManageBeatShaderMaterials : MonoBehaviour
{
    [SerializeField] private Material _material;

    void Update()
    {
        float t = MusicManager.Instance.GetSampleProgress();
        float pulse = 1 - Mathf.Sin(t * Mathf.PI);
        _material.SetFloat("_BeatTime", pulse);
    }
}
