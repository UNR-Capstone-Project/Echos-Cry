using UnityEditor.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameoverManager : MonoBehaviour
{
    [SerializeField] SceneTriggerManager sceneTriggerManager;

    private void Start()
    {
        PlayerStats.OnPlayerDeathEvent += OnMenuEnabled;
    }
    private void OnDestroy()
    {
        PlayerStats.OnPlayerDeathEvent -= OnMenuEnabled;
    }

    public void OnMenuEnabled()
    {
        GetComponent<Canvas>().enabled = true;
        VolumeManager.Instance.SetDepthOfField(true);
        VolumeManager.Instance.SetColorSaturationGrey();
        Time.timeScale = 0f;
    }

    public void OnMenuDisabled()
    {
        EventSystem.current.SetSelectedGameObject(null); //Clear selected button
        GetComponent<Canvas>().enabled = false;
        VolumeManager.Instance.SetDepthOfField(false);
        VolumeManager.Instance.ResetColorSaturation();
        Time.timeScale = 1f;
        
    }

    public void Respawn()
    {
        OnMenuDisabled();
        PlayerStats.Instance.Respawn();
        sceneTriggerManager.StartTransition();
    }
}
