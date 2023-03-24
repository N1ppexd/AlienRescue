using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject settingsPanel;//laitetaan p‰‰lle, kun painetaan settings nappia....

    private void Awake()
    {
        Time.timeScale = 1;
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
        settingsPanel.SetActive(true);
    }
}
