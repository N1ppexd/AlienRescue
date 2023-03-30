using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public PlayerInput playerInput;


    [SerializeField] private Image keyBoardSprite, ControllerSprite;

    private void Awake()
    {
        playerInput.onControlsChanged += _ => ControlsChanged(playerInput.currentControlScheme);
    }

    private void Start()
    {
        ControlsChanged(playerInput.currentControlScheme);
    }

    void ControlsChanged(string currentControlScheme)
    {
        Debug.Log(currentControlScheme + " on controllerScheme nimi");
        if(currentControlScheme == "Gamepad")
        {
            keyBoardSprite.gameObject.SetActive(false);
            ControllerSprite.gameObject.SetActive(true);
        }
        else
        {
            keyBoardSprite.gameObject.SetActive(true);
            ControllerSprite.gameObject.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
