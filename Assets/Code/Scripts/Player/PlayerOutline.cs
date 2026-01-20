using UnityEngine;

public class PlayerOutline : MonoBehaviour
{
    [SerializeField] private Material outlineMaterial;

    void Update()
    {
        float t = MusicManager.Instance.GetSampleProgress();
        float pulse = Mathf.Sin(t * Mathf.PI);
        outlineMaterial.SetFloat("_BeatTime", pulse);
    }
}
