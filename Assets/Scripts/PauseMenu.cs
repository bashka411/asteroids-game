using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isGamePaused = false;
    [SerializeField] GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!isGamePaused) {
                Pause();
            }
            else {
                Resume();
            }
        }
    }

    private void Pause()
    {
        isGamePaused = true;
        Time.timeScale = 0.0f;
        pauseMenuUI.SetActive(true);
    }

    public void Resume()
    {
        isGamePaused = false;
        Time.timeScale = 1.0f;
        pauseMenuUI.SetActive(false);
    }

    public void Restart()
    {
        Resume();
        SceneManager.LoadScene(1);
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quit the game...");
        Application.Quit();
    }
}
