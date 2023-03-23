using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAliens : MonoBehaviour
{

    [SerializeField] private GameObject alienPrefab;//alieni jota spawnataan..

    private GameObject[] spawnPoints; //spawnpoint kohdat, joihin alienit voi spawnata...

    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnpoint");

        Spawn();
    }

    void Spawn()
    {
        foreach(GameObject spawnPoint in spawnPoints)
        {
            Instantiate(alienPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}
