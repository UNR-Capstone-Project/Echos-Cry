using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GraphicsSettingsManager : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _resolutionDropdown;

    private Resolution[] resolutions;

    private void Start()
    {
        //Add supported resolutions.
        resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        foreach (var resolution in resolutions)
        {
            options.Add(resolution.width + " x " + resolution.height);
        }

        _resolutionDropdown.AddOptions(options);

        //Set currently selected dropdown to screens resolution.
        int currentIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentIndex = i;
                break;
            }
        }

        _resolutionDropdown.value = currentIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    public void OnWindowedToggle(bool toggleState)
    {
        Screen.fullScreen = !toggleState;
    }

    public void OnVSyncToggle(bool toggleState)
    {
        QualitySettings.vSyncCount = toggleState ? 1:0;
    }

    public void OnResolutionChanged()
    {
        Resolution currentResolution = resolutions[_resolutionDropdown.value];
        Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
    }
}
