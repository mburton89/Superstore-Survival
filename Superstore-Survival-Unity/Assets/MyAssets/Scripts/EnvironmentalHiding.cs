using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

public class EnvironmentalHiding : MonoBehaviour
{
    [SerializeField] GameObject hidingPlace;
    [SerializeField] GameObject overlay;
    [SerializeField] FirstPersonController player;
    [SerializeField] Image timerBar1;
    [SerializeField] Image timerBar2;

    public bool isHiding = false;
    public float timeRemaining = 15;
    public float maxTime = 15;
    public bool timerIsRunning = false;
    public float speed = 1f;
    public GameObject timerBar;

    void Update()
    {
        if (isHiding == true)
        {
            Hiding();
        }

        if (isHiding == false)
        {
            overlay.SetActive(false);
            timerBar.SetActive(false);
        }

        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                for (float i = speed; i > 0; i--)
                {
                    timeRemaining -= Time.deltaTime;
                }
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                isHiding = false;
                timeRemaining = 15;
            }
        }

        timerBar1.fillAmount = timeRemaining / maxTime;
        timerBar2.fillAmount = timeRemaining / maxTime;
    }

    public void Hiding()
    {
        overlay.SetActive(true);
        timerBar.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHiding = true;
            timerIsRunning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isHiding = false;
            timerIsRunning = false;
            timeRemaining = 15;
        }
    }
}
