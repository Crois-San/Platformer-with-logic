using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    private Resolution[] res;
    private int currentResIndex;
    public Dropdown resolutionSelect;
    // Start is called before the first frame update
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        var resolution = res[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void Start()
    {
        res = Screen.resolutions;
        resolutionSelect.ClearOptions();
        List<string> options = new List<string>();
        int i = 0;
        foreach (var resolution in res)
        {
            string option = resolution.width + "x" + resolution.height;
            options.Add(option);
            if (resolution.Equals(Screen.currentResolution))
            {
                currentResIndex = i;
            }
            i++;
        }
        resolutionSelect.AddOptions(options);
        resolutionSelect.value = currentResIndex;
        resolutionSelect.RefreshShownValue();
    }
}
