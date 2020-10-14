using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class HeartbeatManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip normalBeat;
    public AudioClip chaseBeat;

    public void UpdateSound(EnemySight.State state)
    {
        if (state == EnemySight.State.PATROL)
        {
            audioSource.clip = normalBeat;
        }
        else if (state == EnemySight.State.CHASE)
        {
            audioSource.clip = chaseBeat;
        }

        audioSource.Play();
    }

    public void ManagerUpdateSound(ManagerEnemySight.State state)
    {
        if (state == ManagerEnemySight.State.PATROL)
        {
            audioSource.clip = normalBeat;
        }
        else if (state == ManagerEnemySight.State.CHASE)
        {
            audioSource.clip = chaseBeat;
        }

        audioSource.Play();
    }

    public void StopHeartbeat(Timer timer)
    {
        if (timer.timeRemaining == 0)
        {
            audioSource.Stop();
        }
    }
}
