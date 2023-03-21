using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class BeamDetect : MonoBehaviour
{

    private bool isBeingLifted;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pullForce;

    private Transform ufo;

    [SerializeField] private NavMeshAgent agent; //ai agentti laitetaan pois ku beami ossuu...

    [SerializeField] private bool isUfo;//jos false, ei mene ufon sis‰‰n...

    private void Awake()
    {
        ufo = GameObject.Find("ufo").transform;
    }
    // Update is called once per frame
    void Update()
    {
        if (ufo == null) return;

        if (isBeingLifted)
        {
            if (transform.position.y < ufo.transform.position.y - 3)
                PullAlien();
            else
            {
                isBeingLifted = false;

            }
        }
        else
            rb.useGravity = false;
    }

    private void PullAlien()
    {
        //rb.velocity = (ufo.position - transform.position) * pullForce; //liikutaan beamia kohti tapa 1
        if (rb.useGravity)
            rb.useGravity = false;

        Vector3 ufoVector = (ufo.position - transform.position).normalized; //vektori, joka osoittaa ufoa kohti, jos se sijoitetaan t‰h‰n...

        rb.MovePosition(transform.position + ufoVector * pullForce * Time.deltaTime);    

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("beam"))
        {
            if(agent != null)
                agent.enabled = false;//laitetaan agentti pois p‰‰lt‰
            isBeingLifted = true;
        }
        if (other.gameObject.CompareTag("ufo"))
        {
            if (!isUfo)
                isBeingLifted = false;
            if(isUfo)
                Destroy(gameObject);//t‰m‰ on ihan t‰ytt‰ paskaa mutta nytte teen vaa t‰mmˆsen joka toimii jotenki
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("beam"))
        {
            isBeingLifted = false;
        }
    }
}
