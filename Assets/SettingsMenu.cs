using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{

    private string screenModeString = "screenMode"; // k‰ytet‰‰n playerPrefiss‰...

    [SerializeField] GameObject settingsPanel; //laitetaan pois p‰‰lt‰, kun poistutaan...

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(screenModeString))
        {
            ApplyScreenMode(PlayerPrefs.GetInt(screenModeString));
        }
    } 


    public void ChangeScreenMenu(int mode)
    {
        ApplyScreenMode(mode);
        PlayerPrefs.SetInt(screenModeString, mode);
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


    public void GoBack()//menn‰‰n pois settings menusta...
    {
        settingsPanel.SetActive(false);
    }
}
