using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BeamController : MonoBehaviour
{

    public InputMaster inputMaster;


    [SerializeField] private GameObject beam;
    [SerializeField] private AudioSource beamAudio;//tämä ääni tulee beamista...

    [SerializeField] private float beamMaxDistance = 10, beamAngleMin, beamAngleMax, pullForce = 1000;
    [SerializeField] LayerMask interactableObjects;

    [SerializeField] private MeshRenderer kupoli;
    [SerializeField] private Material normalFace, loveFace, angryFace;

    public static BeamController instance;

    [SerializeField] private AudioSource alienCaptureSound; //äääni, joka kuuluu, kun alieni napataan.

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
            beamAudio.Play();
        }
        else if (!isEnabled && beam != null)
        {
            beam.SetActive(false);
            beamAudio.Stop();
        }

    }

    public void SeenByAPerson()
    {
        StartCoroutine(changeFace(FaceMode.angry));
    }

    enum FaceMode
    {
        angry,
        love
    }
    public void OnCaptureAlien()
    {
        alienCaptureSound.Play();
        StartCoroutine(changeFace(FaceMode.love));
    }

    IEnumerator changeFace(FaceMode mode)
    {
        if(mode == FaceMode.love)
            kupoli.material = loveFace;
        else if (mode == FaceMode.angry)
            kupoli.material = angryFace;
        yield return new WaitForSeconds(1.5f);
        kupoli.material = normalFace;
    }

}
