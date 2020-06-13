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

    private void OnEnable()
    {
        retryButton.onClick.AddListener(HandleRetryPressed);
    }

    private void OnDisable()
    {
        retryButton.onClick.RemoveListener(HandleRetryPressed);
    }

    private void HandleRetryPressed()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void Awake()
    {
        Instance = this;
    }

    public void Show()
    {
        container.SetActive(true);
    }
}