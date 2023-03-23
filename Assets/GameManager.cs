using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private TextMeshProUGUI alienCounterText;

    [SerializeField] private float levelDuration; //kuinka kauan kest‰‰, ett‰ leveli loppuu automaattisesti

    [SerializeField] private int aliensToCapture; //kuink amonta alienia pit‰‰ saada kiinni..

    [SerializeField] private Slider ajastin; //t‰m‰ liikkuu nollaan....

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
        
    }

    // Update is called once per frame
    void Update()
    {
        levelDuration -= Time.deltaTime;
        ajastin.value = levelDuration;

        if(levelDuration <= 0)
        {
            Debug.Log("KUOLIT!!!!");
        }

    }

    public int aliensCaptured;
    public void UpdateAlienCounter()
    {
        aliensCaptured++;
        alienCounterText.text = aliensCaptured.ToString();
    }
}
