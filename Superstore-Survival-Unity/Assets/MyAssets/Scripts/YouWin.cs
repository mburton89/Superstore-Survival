using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;

public class YouWin : MonoBehaviour
{
    public static YouWin Instance;
    public Timer timer;
    public GameObject container;
    public Button nextLevelButton;

    private void OnEnable()
    {
        nextLevelButton.onClick.AddListener(HandleNextLevelPressed);
    }

    private void OnDisable()
    {
        nextLevelButton.onClick.RemoveListener(HandleNextLevelPressed);
    }

    private void HandleNextLevelPressed()
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
        timer.ToggleTime();
    }
}