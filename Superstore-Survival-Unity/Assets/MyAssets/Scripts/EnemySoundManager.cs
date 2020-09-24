using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnemySoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip patrolAudio;
    public AudioClip investigateAudio;
    public AudioClip chaseAudio;

    public void UpdateSound(EnemySight.State state)
    {
        if (state == EnemySight.State.PATROL)
        {
            audioSource.clip = patrolAudio;
        }
        else if (state == EnemySight.State.INVESTIGATE)
        {
            audioSource.clip = investigateAudio;
        }
        else if (state == EnemySight.State.CHASE)
        {
            audioSource.clip = chaseAudio;
        }

        audioSource.Play();
    }
}
