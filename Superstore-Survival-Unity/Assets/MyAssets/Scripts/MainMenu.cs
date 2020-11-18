using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Scene scene;
    AsyncOperation loadingOperation;
    public Slider progressBar;

    private void Update()
    {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
    }
    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void PlayGame()
    {
        loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(scene.name);
    }

}
