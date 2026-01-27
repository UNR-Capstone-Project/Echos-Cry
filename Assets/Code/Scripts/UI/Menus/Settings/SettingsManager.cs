using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public void OnBackButton()
    {
        MenuManager.Instance.SetMenu("Pause");
    }
}
