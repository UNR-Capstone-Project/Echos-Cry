using UnityEngine;
using UnityEngine.EventSystems;

public class GameoverManager : MonoBehaviour
{
    [SerializeField] SceneTriggerManager sceneTriggerManager;
    private void DisableGameoverMenu()
    {
        EventSystem.current.SetSelectedGameObject(null); //Clear selected button
        VolumeManager.Instance.SetDepthOfField(false);
        VolumeManager.Instance.ResetColorSaturation();
        Time.timeScale = 1f;
        MenuManager.Instance.SetMenu("HUD");
    }

    public void Respawn()
    {
        PlayerStats.Instance.Respawn();
        sceneTriggerManager.StartTransition();
        DisableGameoverMenu();
    }
}
