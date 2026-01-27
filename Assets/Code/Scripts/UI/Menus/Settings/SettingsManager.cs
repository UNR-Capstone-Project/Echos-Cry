using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        float currentMasterVolume = AudioManager.Instance.GetMasterLinearVolume();
        float currentMusicVolume = AudioManager.Instance.GetMusicLinearVolume();
        float currentSFXVolume = AudioManager.Instance.GetSFXLinearVolume();
        volumeSlider.value = currentMasterVolume;
        musicSlider.value = currentMusicVolume;
        sfxSlider.value = currentSFXVolume;
    }

    public void OnBackButton()
    {
        MenuManager.Instance.SetMenu("Pause");
    }

    public void ChangeMasterVolume()
    {
        AudioManager.Instance.SetMasterVolume(volumeSlider.value);
    }
    public void ChangeMusicVolume()
    {
        AudioManager.Instance.SetMusicVolume(musicSlider.value);
    }
    public void ChangeSFXVolume()
    {
        AudioManager.Instance.SetSFXVolume(sfxSlider.value);
    }
}
