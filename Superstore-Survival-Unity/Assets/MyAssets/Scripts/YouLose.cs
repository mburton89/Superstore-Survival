using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class YouLose : MonoBehaviour
{
    public static YouLose Instance;
    public GameObject container;
    public Button retryButton;

    //Determine retry level button has been clicked
    private void OnEnable()
    {
        retryButton.onClick.AddListener(HandleRetryPressed);
    }

    //Determine retry level button has not been clicked
    private void OnDisable()
    {
        retryButton.onClick.RemoveListener(HandleRetryPressed);
    }

    //Reload the level
    private void HandleRetryPressed()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void Awake()
    {
        Instance = this;
    }

    //Activate You Lose popup
    public void Show()
    {
        container.SetActive(true);
    }
}