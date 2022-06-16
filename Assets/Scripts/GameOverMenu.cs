using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] GameObject gameOverMenuUI;

    private void Start()
    {
        EventManager.OnGameover.AddListener(() => gameOverMenuUI.SetActive(true));
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
        gameOverMenuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }
}
