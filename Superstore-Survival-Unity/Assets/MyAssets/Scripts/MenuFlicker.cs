using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuFlicker : MonoBehaviour
{
    public GameObject darkScreen;

    int randomNumber;

    private void Update()
    {
        randomNumber = Random.Range(1, 100);

        if (randomNumber < 7)
        {
            darkScreen.SetActive(true);
        }
        else
        {
            darkScreen.SetActive(false);
        }
    }
}
