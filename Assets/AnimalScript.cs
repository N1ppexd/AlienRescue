using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalScript : MonoBehaviour
{

    [SerializeField] private bool doKill;

    [SerializeField] private GameObject characterSprite;

    [SerializeField] private ParticleSystem bloodParticles;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground")) //jos vihu ossuu maahan, eli on tippunut pois beamista, menee agentti taas p‰‰lle...
        {

            if (doKill && isFlying) //paskaa koodia, mutta toimii
            {
                bloodParticles.Play();
                characterSprite.SetActive(false);
                return;
            }
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
