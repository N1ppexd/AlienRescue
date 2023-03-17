using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void PlayGame() //kun painetaan play nappia
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);//ladataan seuraava scene, eli peli
    }

    public void QuitGame()  //kun painetaan Quit Game nappia
    {
        Application.Quit();
    }
}
