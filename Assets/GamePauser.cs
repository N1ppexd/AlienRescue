using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamePauser : MonoBehaviour
{
    private InputMaster inputMaster;

    private bool pauseState;

    [SerializeField] private GameObject pauseScreen;

    // Start is called before the first frame update
    void Awake()
    {
        inputMaster = new InputMaster();

        inputMaster.UI.OpenMenu.performed += _ => PauseScreen();
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }


    private void PauseScreen()
    {
        if(pauseState)
            pauseState = false;
        else if(!pauseState)
            pauseState = true;

        if (pauseState)
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        if (!pauseState)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
