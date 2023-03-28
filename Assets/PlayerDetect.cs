using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerDetect : MonoBehaviour
{

    [SerializeField] private GameObject ufo;
    [SerializeField] private Transform valokeila;
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private EnemyAI enemyAi;

    public float seeRadius;
    public float seeHeightOffset, viewAngle;

    public LayerMask whatIsUfo;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LookForUfo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [SerializeField] private float maxDistanceToUfo = 5;
    private bool isSeen;
    IEnumerator LookForUfo()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            FOVCheck();
        }
        


        
    }
    public Vector3 enemyAxis; //axis...
    private void FOVCheck()
    {
        Vector3 lookPositionVector = transform.position + transform.up * ufo.transform.position.y;
        Collider[] rangeChecks = Physics.OverlapSphere(lookPositionVector, seeRadius, whatIsUfo);

        if(rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 targetDir = target.position - lookPositionVector;

            Vector3 targetDirVector = targetDir.normalized; //en tiedä tarvitaanko.... tämä on suuntavektori ufoon päin vihollisesta katsottuna....
            targetDirVector.y = 0;                          //laitetaan y nollaan... eli ei katsota ylöspäin...


            enemyAxis = new Vector3(enemyAi.axis.x, 0, enemyAi.axis.y);
            float playerRotation = Vector3.Angle(enemyAxis, transform.forward);

            if(Vector3.Angle(transform.position, targetDirVector) < viewAngle - playerRotation / 2)//jatetaan kahdella, koska niin
            {
                Debug.Log("HAAHAA OLET NÄKYVISSÄ....");
                isSeen = true;
                StartCoroutine(takeTimeOff());
            }
        }
    }

    IEnumerator takeTimeOff()
    {
        agent.SetDestination(transform.position);
        valokeila.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.8f);
        Debug.Log("YOU HAVE BEEN SEEN IDIOT!");
        GameManager.instance.levelDuration -= GameManager.instance.levelDuration / 10; //otetaan 10 prosenttia ajasta....

        yield return new WaitForSeconds(0.1f);
        valokeila.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

        isSeen = false;
    }


    public Vector3 dirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }

}
