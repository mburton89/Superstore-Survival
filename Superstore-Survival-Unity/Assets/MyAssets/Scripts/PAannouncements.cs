using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAannouncements : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField] AudioClip[] announcement;
 
    public float delayNumber;

    private void Start()
    {
        delayNumber = 5;
        //delayNumber = Random.Range(20, 40);
    }

    private void Update()
    {
        delayNumber -= Time.deltaTime;

        if (delayNumber <= 0)
        {
            int index = Random.Range(0, announcement.Length);
            audioSource.clip = announcement[index];
            audioSource.Play();
            delayNumber = Random.Range(20, 40);
        }
    }
}
