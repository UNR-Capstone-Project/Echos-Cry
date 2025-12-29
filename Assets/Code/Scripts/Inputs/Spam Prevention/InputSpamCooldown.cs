using UnityEngine;
using System;
using System.Collections;

public class InputSpamCooldown : MonoBehaviour
{
    private int _inputCount = 0;
    private int _maxInputCountPerSec = 1;
    [SerializeField] int _inputPaddingGrace = 4;

    private bool _pauseBeatInputs = false;
    [SerializeField] private float _spamCooldown = 5f;

    private IEnumerator WaitForSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _inputCount = 0;
        }
    }
    private IEnumerator SpamCooldown()
    {
        _pauseBeatInputs = true;
        yield return new WaitForSeconds(_spamCooldown);
        _pauseBeatInputs = false;
    }
    private void UpdateBPMInputCount()
    {
        float timeBetweenBeats = 60f / (float)MusicManager.Instance.GetTempo();
        _maxInputCountPerSec = (int)(1f / timeBetweenBeats) + _inputPaddingGrace;
    }
}
