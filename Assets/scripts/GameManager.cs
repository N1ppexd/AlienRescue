using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI alienCounterText;

    public float levelDuration; //kuinka kauan kest‰‰, ett‰ leveli loppuu automaattisesti
    public float maxLevelDuration;

    [SerializeField] private int aliensToCapture; //kuink amonta alienia pit‰‰ saada kiinni..

    [SerializeField] private Slider ajastin; //t‰m‰ liikkuu nollaan....

    [SerializeField] private GameObject lostScreen, winScreen;

    [SerializeField] private Animator lostScreenAnim;
    [SerializeField] private Animator winScreenAnim;
    [SerializeField] private string lostScreenAnimName;

    public AudioSource glitchAudio;

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
            Time.timeScale = 0f;
            //lostScreenAnim.Play(lostScreenAnimName);
        }

    }

    public int aliensCaptured;
    public void UpdateAlienCounter()
    {
        aliensCaptured++;
        alienCounterText.text = aliensCaptured.ToString();

        if(aliensCaptured >= aliensToCapture)
        {
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
