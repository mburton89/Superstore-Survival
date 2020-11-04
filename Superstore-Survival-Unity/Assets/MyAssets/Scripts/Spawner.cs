using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] items;
    public GameObject spawner;
    private int rand;

    private void Start()
    {
        rand = Random.Range(0, items.Length);
        Instantiate(items[rand], gameObject.transform.position, Quaternion.identity);
        Destroy(spawner);
    }
}
