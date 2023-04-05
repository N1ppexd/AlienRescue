using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class animalAi : MonoBehaviour
{

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float wanderRadius;

    [SerializeField] private LayerMask groundMask;

    private Vector2 axis;

    private Vector3 wanderPos;
    


    void Awake()
    {
        if (agent is null)
            return;

        try
        {
            StartCoroutine(wanderRandomly());
        }
        catch
        {
            Debug.Log("ei toimi");
        }

    }



    IEnumerator wanderRandomly()
    {
        while (true)
        {
            Wander();


            yield return new WaitForSeconds(Random.Range(5, 15));
        }
    }



    void Wander()       //otetaan random sijainti jostain johon liikutaan. Pitt‰‰ muuttaa, koska loppupeliss‰ ne ei varmaankaan liiku n‰in
    {
        if (!agent.enabled) //jos agentti on pois p‰‰lt‰, tehd‰‰n n‰in...
            return;
        //wanderPos = 

        Vector2 pos = Random.insideUnitCircle * wanderRadius; //otetaan vektor 2, joka osoittaa random suuntaan
        axis = pos.normalized;//axis....

        wanderPos = new Vector3(pos.x, transform.position.y, pos.y);

        RaycastHit hit;
        if (Physics.Raycast(wanderPos, Vector3.down, out hit, 10.0f, groundMask))
            wanderPos = new Vector3(pos.x, hit.point.y, pos.y);


        try
        {
            agent.SetDestination(wanderPos); //laitetaan ai:lle t‰m‰ kohteeksi...

        }
        catch
        {
            Debug.Log("ei voida liikkua jostain syyst‰");
        }

    }
}
