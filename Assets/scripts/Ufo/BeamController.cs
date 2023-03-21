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

    private void Awake()
    {
        inputMaster = new InputMaster();
        inputMaster.Enable();

        inputMaster.Player.Beam.performed += _ => BeamAbility(true);
        inputMaster.Player.Beam.canceled += _ => BeamAbility(false);
    }

    void BeamAbility(bool isEnabled)
    {
        Debug.Log("beam" + isEnabled);
        if (isEnabled)
        {
            beam.SetActive(true);
            beamAudio.Play();
        }
        else if (!isEnabled)
        {
            beam.SetActive(false);
            beamAudio.Stop();
        }

    }

}
