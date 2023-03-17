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
    [SerializeField] private LayerMask groundMask; //objektit, joilla on t�m� layer, on maata

    [SerializeField] private SpriteRenderer characterSprite;

    private Vector2 axis; //k�ytet��n siihen, ett� sprite katsoo menosuuntaan...
    private Vector3 wanderPos;

    [SerializeField] private ParticleSystem bloodParticles; //veril�isk�....

    // Start is called before the first frame update
    void Start()
    {
        if(agent != null)
            StartCoroutine(wanderRandomly());
    }


    private void Update()
    {

        //  HUOM: pit�� laittaa viel� niin, ett� jos menee y akselilla enemm�n kuin x:ll�, 
        //niin se animaatio muuttuu siihen, ett� selk� on t�h�n suuntaan, ja toisinp�in my�s

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

    void Wander()       //otetaan random sijainti jostain johon liikutaan. Pitt�� muuttaa, koska loppupeliss� ne ei varmaankaan liiku n�in
    {
        if (!agent.enabled) //jos agentti on pois p��lt�, tehd��n n�in...
            return;
        //wanderPos = 

        Vector2 pos = Random.insideUnitCircle * wanderRadius; //otetaan vektor 2, joka osoittaa random suuntaan
        axis = pos.normalized;//axis....

        wanderPos = new Vector3(pos.x, transform.position.y, pos.y);

        RaycastHit hit;
        if (Physics.Raycast(wanderPos, Vector3.down, out hit, 10.0f, groundMask))
            wanderPos = new Vector3(pos.x, hit.point.y, pos.y);

        agent.SetDestination(wanderPos); //laitetaan ai:lle t�m� kohteeksi...

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!agent.enabled && collision.gameObject.CompareTag("ground")) //jos vihu ossuu maahan, eli on tippunut pois beamista, menee agentti taas p��lle...
        {
            bloodParticles.Play();
            agent.enabled = true;
            StartCoroutine(wanderRandomly());
        }
    }
}
