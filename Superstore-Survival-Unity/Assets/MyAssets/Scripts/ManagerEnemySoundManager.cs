using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ManagerEnemySoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip patrolAudio;
    public AudioClip investigateAudio;
    public AudioClip chaseAudio;

    public void UpdateSound(ManagerEnemySight.State state)
    {
        if (state == ManagerEnemySight.State.PATROL)
        {
            audioSource.clip = patrolAudio;
        }
        else if (state == ManagerEnemySight.State.INVESTIGATE)
        {
            audioSource.clip = investigateAudio;
        }
        else if (state == ManagerEnemySight.State.CHASE)
        {
            audioSource.clip = chaseAudio;
        }

        audioSource.Play();
    }
}
