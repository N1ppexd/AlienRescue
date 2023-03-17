using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float wanderRadius;
    [SerializeField] private LayerMask groundMask; //objektit, joilla on tämä layer, on maata

    [SerializeField] private SpriteRenderer characterSprite;

    private Vector2 axis; //käytetään siihen, että sprite katsoo menosuuntaan...
    private Vector3 wanderPos;

    [SerializeField] private ParticleSystem bloodParticles; //veriläiskä....

    // Start is called before the first frame update
    void Start()
    {
        if(agent != null)
            StartCoroutine(wanderRandomly());
    }


    private void Update()
    {

        //  HUOM: pitää laittaa vielä niin, että jos menee y akselilla enemmän kuin x:llä, 
        //niin se animaatio muuttuu siihen, että selkä on tähän suuntaan, ja toisinpäin myös

        if (axis.x > 0)
            characterSprite.flipX = true;
        else if(axis.x < 0)
        {
            characterSprite.flipX = false;
        }
    }

    IEnumerator wanderRandomly()
    {
        while (true)
        {
            Wander();
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    void Wander()       //otetaan random sijainti jostain johon liikutaan. Pittää muuttaa, koska loppupelissä ne ei varmaankaan liiku näin
    {
        if (!agent.enabled) //jos agentti on pois päältä, tehdään näin...
            return;
        //wanderPos = 

        Vector2 pos = Random.insideUnitCircle * wanderRadius; //otetaan vektor 2, joka osoittaa random suuntaan
        axis = pos.normalized;//axis....

        wanderPos = new Vector3(pos.x, transform.position.y, pos.y);

        RaycastHit hit;
        if (Physics.Raycast(wanderPos, Vector3.down, out hit, 10.0f, groundMask))
            wanderPos = new Vector3(pos.x, hit.point.y, pos.y);

        agent.SetDestination(wanderPos); //laitetaan ai:lle tämä kohteeksi...

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!agent.enabled && collision.gameObject.CompareTag("ground")) //jos vihu ossuu maahan, eli on tippunut pois beamista, menee agentti taas päälle...
        {
            bloodParticles.Play();
            agent.enabled = true;
            StartCoroutine(wanderRandomly());
        }
    }
}
