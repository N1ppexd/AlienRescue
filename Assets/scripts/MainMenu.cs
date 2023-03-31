using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsPanel;//laitetaan päälle, kun painetaan settings nappia....
    [SerializeField] private GameObject settingsDefaultButton, mainMenuDefaultButton;

    [SerializeField] private PlayerInput playerInput;

    private void Awake()
    {
        Time.timeScale = 1;
        playerInput.enabled = true;
        playerInput.onControlsChanged += _ => ControlsChanged(playerInput.currentControlScheme);
    }

    public void PlayGame() //kun painetaan play nappia
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//ladataan seuraava scene, eli peli
    }

    public void QuitGame()  //kun painetaan Quit Game nappia
    {
        Application.Quit();
    }

    public void Settings()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            EventSystem.current.firstSelectedGameObject = null;
            EventSystem.current.SetSelectedGameObject(settingsDefaultButton);
        }
        settingsPanel.SetActive(true);
    }


    void ControlsChanged(string mode)
    {
        if(mode == "Gamepad")
        {
            EventSystem.current.firstSelectedGameObject = null;
            EventSystem.current.SetSelectedGameObject(mainMenuDefaultButton);
        }
    }
}
