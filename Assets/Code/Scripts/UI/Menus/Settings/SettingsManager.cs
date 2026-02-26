using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public event Action OnMenuBackButton;

    public void OnBackButton()
    {
        OnMenuBackButton?.Invoke();
    }
}
