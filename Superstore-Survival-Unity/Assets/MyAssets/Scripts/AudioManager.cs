using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip backgroundMusic;
    public AudioClip CountdownMusic;
    public AudioClip chaseMusic;

    public void UpdateMusic(EnemySight.State state)
    {
        if (state == EnemySight.State.PATROL)
        {
            if (audioSource.clip == backgroundMusic)
            {
                return;
            }
            else
            {
                audioSource.clip = backgroundMusic;
            }
        }
        else if (state == EnemySight.State.CHASE)
        {
            if (audioSource.clip == chaseMusic)
            {
                return;
            }
            else
            {
                audioSource.clip = chaseMusic;
            }
        }

        audioSource.Play();
    }

    public void UpdateMusic(ManagerEnemySight.State state)
    {
        if (state == ManagerEnemySight.State.PATROL)
        {
            if (audioSource.clip != backgroundMusic)
            {
                audioSource.clip = backgroundMusic;
            }
        }
        else if (state == ManagerEnemySight.State.CHASE)
        {
            if (audioSource.clip != chaseMusic)
            {
                audioSource.clip = chaseMusic;
            }
        }

        audioSource.Play();
    }

    public void StartFinalMusic()
    {
        audioSource.clip = CountdownMusic;
        audioSource.Play();
    }
}