using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using URPGlitch.Runtime.AnalogGlitch;
using URPGlitch.Runtime.DigitalGlitch;


public class PlayerDetect : MonoBehaviour
{

    [SerializeField] private GameObject ufo;
    [SerializeField] private Transform valokeila, lookDirTransform; //lookDirTransform on tyhj‰ objecti, joka katsoo siihen suuntaan, mihhin menn‰‰n...
    [SerializeField] private NavMeshAgent agent;

    [SerializeField] private EnemyAI enemyAi;

    public float seeRadius;
    public float seeHeightOffset, viewAngle;

    public float meshResolution;

    public LayerMask whatIsUfo, whatIsObstacle;

    [SerializeField] private MeshFilter meshfilter;
    [SerializeField] private MeshRenderer meshRenderer; //meshfiltteri‰ k‰ytet‰‰n meshiin, meshrendereri‰ meshin materiaaliin
    private Mesh mesh;

    [SerializeField] private Material normalMaterial, redMaterial;

    [SerializeField] private Volume volume;
    private AnalogGlitchVolume analogGlitch;
    private DigitalGlitchVolume digitalGlitch;

    // Start is called before the first frame update
    void Start()
    {

        mesh = new Mesh();
        mesh.name = "fovMesh";
        meshfilter.mesh = mesh;     //laitetaan mehs meshfiltteriin

        StartCoroutine(LookForUfo());
    }

    public Vector3 enemyAxis; //axis...

    private void LateUpdate()
    {
        enemyAxis = new Vector3(enemyAi.axis.x, 0, enemyAi.axis.y);
        lookDirTransform.LookAt(transform.position + enemyAxis * 5);
        DrawFieldOfView();

        
    }

    private bool isSeen;
    IEnumerator LookForUfo()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            FOVCheck();
        }
        


        
    }


    public List<Transform> targets = new List<Transform>(); //targetit, jotka on vihollisen fovin sis‰ll‰.
    private void FOVCheck()
    {

        targets.Clear();//tyhjennet‰‰n...
        Vector3 lookPositionVector = lookDirTransform.position + transform.up * ufo.transform.position.y;
        Collider[] rangeChecks = Physics.OverlapSphere(lookPositionVector, seeRadius, whatIsUfo);

        
        //float playerRotation = Vector3.Angle(enemyAxis, transform.forward);


        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 targetDir = target.position - lookPositionVector;

            Vector3 targetDirVector = targetDir.normalized; //en tied‰ tarvitaanko.... t‰m‰ on suuntavektori ufoon p‰in vihollisesta katsottuna....
            targetDirVector.y = 0;                          //laitetaan y nollaan... eli ei katsota ylˆsp‰in...


            

            if(Vector3.Angle(lookDirTransform.forward, targetDirVector) < viewAngle / 2)//jatetaan kahdella, koska niin
            {
                
                float distToTarget = Vector3.Distance(transform.position + targetDir, transform.position);
                if (!Physics.Raycast(transform.position, targetDir, distToTarget, whatIsObstacle))//jos v‰liss‰ ei ole esteit‰
                {
                    targets.Add(target);//lis‰t‰‰n ufo listaan..
                    Debug.Log("HAAHAA OLET NƒKYVISSƒ....");
                    meshRenderer.material = redMaterial;
                    isSeen = true;
                    StartCoroutine(takeTimeOff());
                    StartCoroutine(GlitchEffect());
                    GameManager.instance.kelloAnim.SetBool("alert", true);
                }
                else
                {
                    isSeen = false;
                    
                    //GameManager.instance.glitchAudio.Stop();                                //lopettaa glitch ‰‰nen
                    meshRenderer.material = normalMaterial;
                }
            }
            else
            {
                
                //GameManager.instance.glitchAudio.Stop();                                //lopettaa glitch ‰‰nen
                isSeen = false;
                meshRenderer.material = normalMaterial;
            }
        }
        else
        {
            //GameManager.instance.glitchAudio.Stop();                                //lopettaa glitch ‰‰nen ihan sama t‰‰ on aika paskasti tehty
            isSeen = false;
            meshRenderer.material = normalMaterial;
        }
    }

    IEnumerator takeTimeOff()
    {
        agent.SetDestination(transform.position);
        valokeila.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        GameManager.instance.glitchAudio.Play();                                //tekee glitch ‰‰nen...
        yield return new WaitForSeconds(0.8f);
        Debug.Log("YOU HAVE BEEN SEEN IDIOT!");


        GameManager.instance.levelDuration -= GameManager.instance.levelDuration / 10; //otetaan 10 prosenttia ajasta....

        yield return new WaitForSeconds(0.1f);
        valokeila.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
        GameManager.instance.kelloAnim.SetBool("alert", false);


    }


    IEnumerator GlitchEffect()
    {
        volume.profile.TryGet<AnalogGlitchVolume>(out analogGlitch);
        volume.profile.TryGet<DigitalGlitchVolume>(out digitalGlitch);

        analogGlitch.active = true; digitalGlitch.active = true;

        FaceChanger.instance.SeenByAPerson(); //TODELLA paskaa koodia, mutta korjaa tulevaisuudessas

        yield return new WaitForSeconds(1f);

        GameManager.instance.glitchAudio.Stop();                                //lopettaa glitch ‰‰nen
        analogGlitch.active = false; digitalGlitch.active = false;
    }

    void DrawFieldOfView() //tehd‰‰n mesh...
    {
        int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= stepCount; i++)
        {
            float angle = lookDirTransform.eulerAngles.y - viewAngle/2 + stepAngleSize*i;
            ViewCastInfo newViewCast = ViewCast(angle);
            viewPoints.Add(newViewCast.pos); //lis‰t‰‰n sijainti listaan...
        }

        int vertexCount = viewPoints.Count + 1;//lis‰t‰‰n yhdell‰ koska transform.position on yksi vertex, ja tehd‰‰n siis mesh joka on ‰h‰n kolmion n‰kˆinen...
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3]; //kolmioiden m‰‰r‰...

        vertices[0] = Vector3.zero;

        for(int i = 0; i < vertexCount - 1; i++)//miinustetaan yhdell‰, koska yksi vertex on jo laitettu...
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
                //      LAITETAAN KOLMIOIDEN jutut
            if(i < vertexCount - 2)
            {
                triangles[i * 3] = 0;
                triangles[i * 3 + 1] = i + 1;
                triangles[i * 3 + 2] = i + 2;
            }
            
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 dir = dirFromAngle(globalAngle, true);
        RaycastHit hit;
        if(Physics.Raycast(transform.position, dir, out hit, seeRadius, whatIsObstacle))
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        }
        else
        {
            return new ViewCastInfo(false, transform.position + dir * seeRadius, seeRadius, globalAngle);
        }
    }
    public Vector3 dirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
            angleInDegrees += lookDirTransform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }


    public struct ViewCastInfo
    {
        public bool hit;        //osuuko johonkin
        public Vector3 pos;     //rayn lopun sijanti
        public float 
            distance,           //et‰isyys 
            angle;              //kulma

        public ViewCastInfo(bool _hit, Vector3 _pos, float _distance, float _angle)
        {
            hit = _hit;
            pos = _pos;
            distance = _distance;
            angle = _angle;

        }
    }

}
