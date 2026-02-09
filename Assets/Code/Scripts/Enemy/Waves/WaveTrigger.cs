using UnityEngine;
/// Original Author: Victor
/// All Contributors Since Creation: Victor
/// Last Modified By:
public class WaveTrigger : MonoBehaviour
{
    [SerializeField] private WaveManager _waveManager;
    bool waveHasStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!waveHasStarted)
        {
            _waveManager.StartNextWave();
            waveHasStarted = true;
        }
    }  
}
