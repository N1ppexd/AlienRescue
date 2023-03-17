using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float wanderRadius;
    [SerializeField] private LayerMask groundMask; //objektit, joilla on t‰m‰ layer, on maata

    private Vector3 wanderPos;

    // Start is called before the first frame update
    void Start()
    {
        if(agent != null)
            StartCoroutine(wanderRandomly());
    }


    IEnumerator wanderRandomly()
    {
        while (true)
        {
            Wander();
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    void Wander()       //otetaan random sijainti jostain johon liikutaan. Pitt‰‰ muuttaa, koska loppupeliss‰ ne ei varmaankaan liiku n‰in
    {
        if (!agent.enabled) //jos agentti on pois p‰‰lt‰, tehd‰‰n n‰in...
            return;
        //wanderPos = 

        Vector2 pos = Random.insideUnitCircle * wanderRadius; //otetaan vektor 2, joka osoittaa random suuntaan


        wanderPos = new Vector3(pos.x, transform.position.y, pos.y);

        RaycastHit hit;
        if (Physics.Raycast(wanderPos, Vector3.down, out hit, 10.0f, groundMask))
            wanderPos = new Vector3(pos.x, hit.point.y, pos.y);

        agent.SetDestination(wanderPos); //laitetaan ai:lle t‰m‰ kohteeksi...

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!agent.enabled && collision.gameObject.CompareTag("ground")) //jos vihu ossuu maahan, eli on tippunut pois beamista, menee agentti taas p‰‰lle...
        {
            agent.enabled = true;
            StartCoroutine(wanderRandomly());
        }
    }
}
