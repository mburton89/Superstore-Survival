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
    public Text percentLoaded;
    public CanvasGroup customizationPanel;

    private void Update()
    {
        progressBar.value = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
        percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
    }
    private void Awake()
    {
        scene = SceneManager.GetActiveScene();
    }

    public void PlayGame()
    {
        loadingOperation = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        StartCoroutine(FadeLoad(0.25f));
    }

    IEnumerator FadeLoad(float duration)
    {
        float startValue = customizationPanel.alpha;
        float time = 0;

        while (time < duration)
        {
            customizationPanel.alpha = Mathf.Lerp(startValue, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        customizationPanel.alpha = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(scene.name,LoadSceneMode.Single);
    }

}
