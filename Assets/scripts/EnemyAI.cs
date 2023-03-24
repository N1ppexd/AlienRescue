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
    [SerializeField] private LayerMask groundMask; //objektit, joilla on t‰m‰ layer, on maata

    [SerializeField] private SpriteRenderer characterSprite;

    private Vector2 axis; //k‰ytet‰‰n siihen, ett‰ sprite katsoo menosuuntaan...
    private Vector3 wanderPos;

    [SerializeField] private bool doKill; //jos true, hahmo kuolee...

    

    [SerializeField] private ParticleSystem bloodParticles; //veril‰isk‰....


    [SerializeField] private Animator anim;

    [SerializeField] private Transform valokeila;//valokeila transform....

    [SerializeField] private GameObject ufo; //ufo objekti...


    // Start is called before the first frame update
    void Awake()
    {
        if (agent is null)
            return;
        StartCoroutine(wanderRandomly());
        ufo = GameObject.Find("ufo");

    }


    private void FixedUpdate()
    {
        anim.SetFloat("x", axis.x);
        anim.SetFloat("y", axis.y);

        if(axis.x > 0)
        {
            valokeila.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            valokeila.transform.localScale = new Vector3(1, 1, 1);
        }

        LookForUfo();
    }

    [SerializeField] private float maxDistanceToUfo = 10;
    private bool isSeen;
    private void LookForUfo()
    {
        Vector3 u = ufo.transform.position - transform.position; //t‰m‰ on vektori, joka osoittaa ufoa kohti vihusta....
        u.y = transform.position.y;
        float uLength = u.magnitude; //otetaan t‰m‰, jotta saadaan selville, kuinka kaukana ufo on vihollisesta..
        u = u.normalized;   //normalisoidaan, jotta saadaan vain suuntavektori.


        if(u.x > 0 && axis.x > 0 && !isSeen)
        {
            if(uLength <= maxDistanceToUfo)
            {
                isSeen = true;
                StartCoroutine(takeTimeOff());   
            }
        }
        else if (u.x <= 0 && axis.x <= 0 && !isSeen)
        {
            if (uLength <= maxDistanceToUfo)
            {
                isSeen=true;
                StartCoroutine(takeTimeOff());
            }
        }
    }

    IEnumerator takeTimeOff()
    {
        agent.SetDestination(transform.position);
        valokeila.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.4f);
        Debug.Log("YOU HAVE BEEN SEEN IDIOT!");
        GameManager.instance.levelDuration -= GameManager.instance.levelDuration / 10; //otetaan 10 prosenttia ajasta....

        yield return new WaitForSeconds(0.1f);
        valokeila.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        isSeen = false;
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
        axis = pos.normalized;//axis....

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

            if (doKill && isFlying) //paskaa koodia, mutta toimii
            {
                bloodParticles.Play();
                characterSprite.gameObject.SetActive(false);
                return;
            }
            
            isFlying = false;
            agent.enabled = true;
            StartCoroutine(wanderRandomly());
        }
    }

    private bool isFlying;

    private void OnCollisionExit()
    {
        StartCoroutine(waitLeaveGround());
    }

    IEnumerator waitLeaveGround()
    {
        yield return new WaitForSeconds(0.2f);
        isFlying = true;
    }
    IEnumerator killCharacter()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
