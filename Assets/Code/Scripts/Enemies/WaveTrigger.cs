using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    private enum waveState
    {
        Idle, 
        Active
    }

    private waveState state;

    private void Awake()
    {
        state = waveState.Idle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == PlayerRef.PlayerTransform)
        {
            if (state == waveState.Idle)
            {
                state = waveState.Active;
            }
        }
    }
}
