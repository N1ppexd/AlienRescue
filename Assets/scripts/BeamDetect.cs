using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BeamDetect : MonoBehaviour
{

    private bool isBeingLifted;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pullForce;

    private Transform ufo;

    [SerializeField] private NavMeshAgent agent; //ai agentti laitetaan pois ku beami ossuu...

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
            if (transform.position.y < ufo.transform.position.y - 2)
                rb.velocity = (ufo.position - transform.position) * pullForce; //liikutaan beamia kohti
            else
            {
                isBeingLifted = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("beam"))
        {
            agent.enabled = false;//laitetaan agentti pois päältä
            isBeingLifted = true;
        }
        if (other.gameObject.CompareTag("ufo"))
        {
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
}
