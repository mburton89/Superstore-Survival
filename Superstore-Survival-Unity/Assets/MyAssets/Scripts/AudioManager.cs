using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip backgroundMusic;
    public AudioClip CountdownMusic;

    public void PlayMusic(Timer timer)
    {
        audioSource.clip = backgroundMusic;

        if(timer.timeRemaining == 0)
        {
            audioSource.clip = CountdownMusic;
        }
    }
}
