using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI alienCounterText;

    public float levelDuration; //kuinka kauan kest��, ett� leveli loppuu automaattisesti
    public float maxLevelDuration;

    [SerializeField] private int aliensToCapture; //kuink amonta alienia pit�� saada kiinni..

    [SerializeField] private Slider ajastin; //t�m� liikkuu nollaan....
    [SerializeField] Image ajastinFill;

    [SerializeField] private GameObject lostScreen, winScreen;

    [SerializeField] private Animator lostScreenAnim;
    [SerializeField] private Animator winScreenAnim;
    [SerializeField] private string lostScreenAnimName;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject winScreenDefaultButton, PauseScreenDefaultButton, LoseScreenDefaultButton;

    public AudioSource glitchAudio;
    public Animator kelloAnim;

    [SerializeField] private AudioSource[] audios;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
            Destroy(this);
        
    }


    // Start is called before the first frame update
    void Start()
    {
        aliensToCapture = GameObject.FindGameObjectsWithTag("alien").Length;

        alienCounterText.text = aliensCaptured.ToString() + " / " + aliensToCapture.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        levelDuration -= Time.deltaTime;
        ajastin.value = levelDuration;

        if(levelDuration <= 0)
        {
            Debug.Log("KUOLIT!!!!");
            lostScreen.SetActive(true);
            if (playerInput.currentControlScheme == "Gamepad")
            {
                EventSystem.current.firstSelectedGameObject = null;
                EventSystem.current.SetSelectedGameObject(LoseScreenDefaultButton);
            }

            foreach(AudioSource audio in audios) //laitetaan musiikit sun muut pois p��lt�
                audio.Stop();
                
            Time.timeScale = 0f;
            //lostScreenAnim.Play(lostScreenAnimName);
        }

        if(levelDuration <= maxLevelDuration / 5) //jos aikaa on alle tai yht�paljonl, kuin levelin maksimi ajasta 20 prosenttia....
        {
            ajastinFill.color = Color.red;
        }

    }

    public int aliensCaptured;
    public void UpdateAlienCounter()
    {
        aliensCaptured++;
        alienCounterText.text = aliensCaptured.ToString() + " / " + aliensToCapture.ToString();

        if(aliensCaptured >= aliensToCapture)
        {
            winScreen.SetActive(true);

            if (playerInput.currentControlScheme == "Gamepad")
            {
                EventSystem.current.firstSelectedGameObject = null;
                EventSystem.current.SetSelectedGameObject(winScreenDefaultButton);
            }

            foreach (AudioSource audio in audios) //laitetaan musiikit sun muut pois p��lt�
                audio.Stop();

            Time.timeScale = 0;
        }
    }
}
