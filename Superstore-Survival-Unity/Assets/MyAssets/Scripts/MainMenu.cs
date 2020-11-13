using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Scene scene;
    public GameObject fadeObject;
    private Image fadeImage;

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(scene.name);
    }

}
