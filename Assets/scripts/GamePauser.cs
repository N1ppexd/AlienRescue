using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GamePauser : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private InputMaster inputMaster;

    private bool pauseState;

    [SerializeField] private GameObject pauseScreen, 
        defaultSelectedObj, 
        optionsDefaultSelectedObj;//default selected obj on defaultti homma joka mennee p‰llle, kun k‰ˆytetˆˆn oihjainta

    // Start is called before the first frame update
    void Awake()
    {
        inputMaster = new InputMaster();

        inputMaster.UI.OpenMenu.performed += _ => PauseScreen(); //kun pausetetaan menu, pausetetaan menu ja silleen niin jee mkoahtavaa
        playerInput.onControlsChanged += _ => OnControlsChanged(playerInput.currentControlScheme); //kun playerinputti havaitsee ett‰ juttuja on muutettu nii sitte tehh‰‰n t‰lleen
    }

    private void Start()
    {
        OnControlsChanged(playerInput.currentControlScheme);//tehd‰‰n alussa, koska niin-...
    }

    private void OnEnable()
    {
        inputMaster.Enable();
    }

    private void OnDisable()
    {
        inputMaster.Disable();
    }

    private bool isGamepad;

    void OnControlsChanged(string mode)
    {
        if(mode == "Gamepad")
        {
            isGamepad = true;

            if (pauseState)
                SetDefaultSelectedObj();
        }
        else
            isGamepad = false;
    }

    public void PauseScreen()
    {
        if(pauseState)              //tm‰ p‰tk‰ koodia t‰ss‰ on maailman idioottisin p‰tk‰ miss‰‰n
            pauseState = false;
        else if(!pauseState)
            pauseState = true;

        if (pauseState)
        {
            pauseScreen.SetActive(true);

            if (isGamepad)
            {
                SetDefaultSelectedObj();
            }

            Time.timeScale = 0;
        }
        if (!pauseState)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1.0f;
        }

    }

    public void OptionsMenu()
    {

    }

    void SetDefaultSelectedObj()
    {
        EventSystem.current.firstSelectedGameObject = null;
        EventSystem.current.SetSelectedGameObject(defaultSelectedObj); //laitetaan t‰m‰ p‰‰lle...
    }
}
