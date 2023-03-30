using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TutorialScript : MonoBehaviour
{

    public PlayerInput playerInput;

    [SerializeField] private Sprite KeyboardSprite, ControllerSprite;

    [SerializeField] private Image spriteImage;

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
            spriteImage.sprite = ControllerSprite;
        }
        else
        {
            spriteImage.sprite = KeyboardSprite;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
