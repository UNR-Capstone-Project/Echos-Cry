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
    public void SetMusicVolume(float linearVolume)
    {
        float dbVolume;
        if (linearVolume <= 0) { dbVolume = -80f; }
        else { dbVolume = Mathf.Log10(linearVolume) * 20f; }

        masterMixer.SetFloat("MusicVolume", dbVolume);
    }
    public void SetSFXVolume(float linearVolume)
    {
        float dbVolume;
        if (linearVolume <= 0) { dbVolume = -80f; }
        else { dbVolume = Mathf.Log10(linearVolume) * 20f; }

        masterMixer.SetFloat("SFXVolume", dbVolume);
    }

    public float GetMasterLinearVolume()
    {
        if (masterMixer.GetFloat("MasterVolume", out float dbVolume))
        {
            float linearVolume = Mathf.Pow(10f, dbVolume / 20f);
            return linearVolume;
        }
        return 1f;
    }
    public float GetMusicLinearVolume()
    {
        if (masterMixer.GetFloat("MusicVolume", out float dbVolume))
        {
            float linearVolume = Mathf.Pow(10f, dbVolume / 20f);
            return linearVolume;
        }
        return 1f;
    }
    public float GetSFXLinearVolume()
    {
        if (masterMixer.GetFloat("SFXVolume", out float dbVolume))
        {
            float linearVolume = Mathf.Pow(10f, dbVolume / 20f);
            return linearVolume;
        }
        return 1f;
    }
}
