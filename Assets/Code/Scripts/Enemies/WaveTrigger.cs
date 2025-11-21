using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public WaveManager _waveManager;
    private enum waveTriggerState
    {
        Idle, 
        Active
    }

    private waveTriggerState state;

    private void Awake()
    {
        state = waveTriggerState.Idle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == PlayerRef.PlayerTransform)
        {
            if (state == waveTriggerState.Idle)
            {
                state = waveTriggerState.Active;
                _waveManager.startNewWave();
            }
        }
    }
}
