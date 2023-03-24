using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    private string screenModeString = "screenMode", screenResolutionString = "res"; // k‰ytet‰‰n playerPrefiss‰...

    [SerializeField] GameObject settingsPanel; //laitetaan pois p‰‰lt‰, kun poistutaan...

    [SerializeField] Dropdown resolution, windowMode;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(screenModeString))
        {
            int mode = PlayerPrefs.GetInt(screenModeString);
            ApplyScreenMode(mode);
            windowMode.SetValueWithoutNotify(mode);
        }
        if (PlayerPrefs.HasKey(screenResolutionString))
        {
            int mode = PlayerPrefs.GetInt(screenResolutionString);
            ApplyResolution(mode);
            resolution.SetValueWithoutNotify(mode);
        }
    } 


    public void ChangeScreenMenu(int mode)
    {
        ApplyScreenMode(mode);
        PlayerPrefs.SetInt(screenModeString, mode);
    }
    public void ChangeResolution(int mode)
    {
        ApplyResolution(mode);
        PlayerPrefs.SetInt(screenResolutionString, mode);
    }


    private void ApplyScreenMode(int mode)//t‰‰ll‰ oikeasti applyataan screenMode....
    {
        switch (mode)
        {
            case 0:
                //fullscreen
                Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
                return;
            case 1:
                //windowed
                Screen.fullScreenMode = FullScreenMode.Windowed;
                return;
            case 2:
                //fulscreen windowed
                Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
                return;
        }

        Debug.LogError("screenModea ei ole... ei vaihdeta..");
    }

    private void ApplyResolution(int mode)//t‰‰ll‰ oikeasti applyataan screenMode....
    {
        switch (mode)
        {
            case 0:
                //1920 x 1080
                Screen.SetResolution(1920, 1080, Screen.fullScreen);
                return;
            case 1:
                //1280 x 720
                Screen.SetResolution(1280, 720, Screen.fullScreen);
                return;
            case 2:
                //2560 x 1440
                Screen.SetResolution(2560, 1440, Screen.fullScreen);
                return;
        }

        Debug.LogError("screenModea ei ole... ei vaihdeta..");
    }


    public void GoBack()//menn‰‰n pois settings menusta...
    {
        settingsPanel.SetActive(false);
    }
}
