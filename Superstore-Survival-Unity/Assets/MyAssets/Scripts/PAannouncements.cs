using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAannouncements : MonoBehaviour
{
#pragma warning disable 0649
    public AudioSource audioSource;
    [SerializeField] AudioClip[] announcement; 
    public float delayNumber = 20;
    public bool[] clipPlayed;

    private void Start()
    {
        for (int i = 0; i < announcement.Length; i++)
        {
            clipPlayed[i] = false;
        }
    }

    private void Update()
    {
        delayNumber -= Time.deltaTime;

        if (delayNumber <= 0)
        {
            int index = Random.Range(0, announcement.Length);
            
            if (clipPlayed[index] == false)
            {
                audioSource.clip = announcement[index];
                audioSource.Play();
                clipPlayed[index] = true;
                delayNumber = Random.Range(30, 50);
            }
            else
            {
                delayNumber = .01f;
            }
        }
    }
}
