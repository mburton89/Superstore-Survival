using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] baseItems;
    private GameObject[] possibleSpawners;
    private GameObject spawner;
    private int itemToSpawn;
    public int numberToSpawn;


    private void Update()
    {
        if (numberToSpawn != 0)
        {
            possibleSpawners = GameObject.FindGameObjectsWithTag("Spawn Location");
            itemToSpawn = Random.Range(0, baseItems.Length);
            spawner = possibleSpawners[Random.Range(0, possibleSpawners.Length)];
            Instantiate(baseItems[itemToSpawn], spawner.transform.position, Quaternion.identity);
            Destroy(spawner);
            numberToSpawn -= 1;
        }

    }
}
