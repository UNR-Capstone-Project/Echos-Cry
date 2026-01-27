using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void OnEnable()
    {
        float currentVolume = AudioManager.Instance.GetMasterLinearVolume();
        volumeSlider.value = currentVolume;
    }

    public void OnBackButton()
    {
        MenuManager.Instance.SetMenu("Pause");
    }

    public void ChangeVolume(float volume)
    {
        AudioManager.Instance.SetMasterVolume(volume);
    }
}
