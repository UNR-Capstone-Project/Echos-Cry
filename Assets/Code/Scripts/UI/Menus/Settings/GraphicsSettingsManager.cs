using UnityEngine;

public class GraphicsSettingsManager : MonoBehaviour
{
    public void OnWindowedToggle(bool toggleState)
    {
        Screen.fullScreen = !toggleState;
    }
}
