using UnityEngine;

public class ComboStateMachine : MonoBehaviour
{
    public ComboState CurrentState = null;

    private void Start()
    {
        if (CurrentState == null)
        {
            Debug.LogError("ComboStateMachine's CurrentState is unset and the ComboSystem will be disabled!");
            this.enabled = false;
        }
    }
}
