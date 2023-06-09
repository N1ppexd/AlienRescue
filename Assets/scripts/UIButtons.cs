using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void MainMenu()  //kun painetaan mainmenu nappia
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);//menn��n mainmenuun
    }

    public void QuitGame()  //kun painetaan Quit game nappia
    {
        Application.Quit(); //poistutaan koko pelist�...
    }


    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameTest");
    }

    public void RestartLvl()
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
