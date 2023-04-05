using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{

    private string screenModeString = "screenMode", screenResolutionString = "res"; // k‰ytet‰‰n playerPrefiss‰...

    [SerializeField] GameObject settingsPanel; //laitetaan pois p‰‰lt‰, kun poistutaan...
    [SerializeField] GameObject defaultDefaultButton; //perus btton

    [SerializeField] TMP_Dropdown resolution, windowMode;

    [SerializeField] Slider musicVolSlider, effectsVolSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey(screenModeString))
        {
            int mode = PlayerPrefs.GetInt(screenModeString);
            ApplyScreenMode(mode);
            windowMode.value = mode;
        }
        if (PlayerPrefs.HasKey(screenResolutionString))
        {
            int mode = PlayerPrefs.GetInt(screenResolutionString);
            ApplyResolution(mode);
            resolution.value =  mode;
        }

        if (PlayerPrefs.HasKey("musicVol"))
        {
            float volume = PlayerPrefs.GetFloat("musicVol");
            musicVolSlider.value = volume;

            if (volume <= 0)
                music.SetFloat("volume", -80);
            else
                music.SetFloat("volume", Mathf.Log(volume) * 20);
        }
        if (PlayerPrefs.HasKey("effectsVol"))
        {
            float volume = PlayerPrefs.GetFloat("effectsVol");
            effectsVolSlider.value = volume;

            if (volume <= 0)
                soundEffects.SetFloat("volume", -80);
            else
                soundEffects.SetFloat("volume", Mathf.Log(volume) * 20);
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


    [SerializeField] private AudioMixer soundEffects, music;

    public void SoundEffectVolume(float volume)
    {
        if(volume<=0)
            soundEffects.SetFloat("volume", -80);
        else
            soundEffects.SetFloat("volume", Mathf.Log(volume) * 20);

        PlayerPrefs.SetFloat("effectsVol", volume);
    }

    public void musicVolume(float volume)
    {
        if (volume <= 0)
            music.SetFloat("volume", -80);
        else
            music.SetFloat("volume", Mathf.Log(volume) * 20);

        PlayerPrefs.SetFloat("musicVol", volume);
    }

    public void GoBack()//menn‰‰n pois settings menusta...
    {
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(defaultDefaultButton);
        settingsPanel.SetActive(false);
    }
}
