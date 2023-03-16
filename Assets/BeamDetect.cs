using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamDetect : MonoBehaviour
{

    private bool isBeingLifted;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float pullForce;

    private Transform ufo;

    private void Awake()
    {
        ufo = GameObject.Find("ufo").transform;
    }
    // Update is called once per frame
    void Update()
    {

        if (isBeingLifted)
        {
            rb.AddForce((ufo.position - transform.position) * pullForce * Time.deltaTime, ForceMode.Force);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("beam"))
        {
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
