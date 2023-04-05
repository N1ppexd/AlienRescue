using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryText : MonoBehaviour
{

    InputMaster inputMaster;
    private void Awake()
    {
        inputMaster = new InputMaster();

        inputMaster.Player.fastMove.performed += _ => EndTexts();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }
    public void EndTexts()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
