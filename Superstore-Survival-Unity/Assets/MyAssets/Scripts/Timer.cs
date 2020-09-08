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

    //Timer counts down
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
            //When timer reaches zero display a You Win popup
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                YouWin.Instance.Show();
            }
        }
    }

    //Display the timer as UI
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    //Stop game and movement
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

    //Make cursor appear
    public void showCursor()
    {
        Player.GetComponent<FirstPersonController>().enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}