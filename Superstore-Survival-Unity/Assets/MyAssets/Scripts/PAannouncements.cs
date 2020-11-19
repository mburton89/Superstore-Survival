using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAannouncements : MonoBehaviour
{
    public AudioSource audioSource;

    [SerializeField] AudioClip[] announcement;
 
    float delayNumber;

    private void Start()
    {

    }

    public void PlayAnnouncement()
    {
        Delay();

        if (delayNumber <= 0)
        {
            audioSource.Play();
        }
    }

    public void Delay()
    {
        delayNumber = Random.Range(20, 40);

        delayNumber -= Time.deltaTime;

        if (delayNumber <= 0)
        {
            delayNumber = 0;
        }
    }
}
