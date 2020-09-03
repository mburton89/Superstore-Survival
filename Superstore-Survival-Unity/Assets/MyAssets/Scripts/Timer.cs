using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public float speed = 1f;
    private bool pauseGame = false;
    public GameObject Player;
    
    private void Start()
    {
        // Starts the timer automatically
        timerIsRunning = true;
    }

    public void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                for (float i = speed; i > 0; i--)
                {
                    timeRemaining -= Time.deltaTime;
                }
                DisplayTime(timeRemaining);
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                YouWin.Instance.Show();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void ToggleTime()
    {
        pauseGame = !pauseGame;

        if (pauseGame)
        {
            Player.GetComponent<FirstPersonController>().enabled = false;
            Time.timeScale = 0;
            showCursor();
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    public void showCursor()
    {
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}