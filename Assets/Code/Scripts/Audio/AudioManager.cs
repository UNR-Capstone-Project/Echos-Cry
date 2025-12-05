using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioMixer masterMixer;

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

    public void SetMasterVolume(float linearVolume)
    {
        float dbVolume;
        if (linearVolume <= 0) { dbVolume = -80f; }
        else { dbVolume = Mathf.Log10(linearVolume) * 20f; }

        masterMixer.SetFloat("MasterVolume", dbVolume);
    }
}
