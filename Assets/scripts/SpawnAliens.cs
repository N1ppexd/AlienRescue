using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAliens : MonoBehaviour
{

    [SerializeField] private GameObject alienPrefab;//alieni jota spawnataan..

    private GameObject[] spawnPoints; //spawnpoint kohdat, joihin alienit voi spawnata...

    [SerializeField] private int amountOfAliens;

    // Start is called before the first frame update
    void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("spawnpoint");

        Spawn();
    }

    List<GameObject> spawnPointList = new List<GameObject>();
    void Spawn()
    {
        for(int i = 0; i < amountOfAliens; i++)
        {
            int j = Random.Range(0, spawnPoints.Length);
            if (!spawnPointList.Contains(spawnPoints[j]))
            {
                spawnPointList.Add(spawnPoints[j]);
            }
            else i--;
        }
        foreach(GameObject spawnPoint in spawnPointList)
        {
            Instantiate(alienPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }
}
