using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BeamController : MonoBehaviour
{

    public InputMaster inputMaster;


    [SerializeField] private GameObject beam;
    [SerializeField] private AudioSource beamAudio;//t�m� ��ni tulee beamista...

    [SerializeField] private float beamMaxDistance = 10, beamAngleMin, beamAngleMax, pullForce = 1000;
    [SerializeField] LayerMask interactableObjects;

    public static BeamController instance;

    [SerializeField] private AudioSource alienCaptureSound; //���ni, joka kuuluu, kun alieni napataan.

    [SerializeField] private Animator beamAnim;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(instance);

        inputMaster = new InputMaster();
        inputMaster.Enable();

        inputMaster.Player.Beam.performed += _ => BeamAbility(true);
        inputMaster.Player.Beam.canceled += _ => BeamAbility(false);
    }

    void BeamAbility(bool isEnabled)
    {
        Debug.Log("beam" + isEnabled);
        if (isEnabled && beam != null)
        {
            beam.SetActive(true);
            StopAllCoroutines();
            beamAnim.Play("BeamStart");
            beamAudio.Play();
        }
        else if (!isEnabled && beam != null)
        {
            beamAnim.Play("BeamStop");

            StartCoroutine(wait());

            
        }

    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.2f);
        beam.SetActive(false);
        beamAudio.Stop();
    }
    public void OnCaptureAlien()
    {
        alienCaptureSound.Play();
    }
}
