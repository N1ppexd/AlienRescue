using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BeamDetect : MonoBehaviour
{

    private bool isBeingLifted, stay; //stay ons siihen, kun osutaan beamiin
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pullForce;

    private Transform ufo;

    [SerializeField] private NavMeshAgent agent; //ai agentti laitetaan pois ku beami ossuu...

    [SerializeField] private bool isUfo;//jos false, ei mene ufon sisään...

    private void Awake()
    {
        ufo = GameObject.Find("ufo").transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (ufo == null) return;

        if (stay)
        {
            isBeingLifted = true;
            PullAlien();
            stay = false;
        }
        else
            isBeingLifted = false;

        if (!isBeingLifted)
            rb.useGravity = true;
    }

    private void PullAlien()
    {
        //rb.velocity = (ufo.position - transform.position) * pullForce; //liikutaan beamia kohti tapa 1
        if (rb.useGravity)
            rb.useGravity = false;

        Vector3 ufoVector = ((ufo.position- (ufo.transform.up * 3)) - transform.position).normalized; //vektori, joka osoittaa ufoa kohti, jos se sijoitetaan tähän...

        rb.MovePosition(transform.position + ufoVector * pullForce * Time.deltaTime);    

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("beam"))
        {
            isBeingLifted = true;
        }
        if (other.gameObject.CompareTag("ufo"))
        {
            if (!isUfo)
                isBeingLifted = false;
            if (isUfo)
                Destroy(gameObject);//tämä on ihan täyttä paskaa mutta nytte teen vaa tämmösen joka toimii jotenki
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("beam"))
        {
            isBeingLifted = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("beam"))
        {
            stay = true;
        }
    }
}
