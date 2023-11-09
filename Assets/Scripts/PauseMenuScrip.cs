using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScrip : MonoBehaviour
{
    [SerializeField] private bool gamesIsPaused;

    [SerializeField] private GameObject pauseMenu;

    [SerializeField] private GlobalVariables globalVariables;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gamesIsPaused)
            {
                Resume();
                globalVariables.InPauseMenuFalse();
            }
            else
            {
                Pause();
                globalVariables.InPauseMenuTrue();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gamesIsPaused = false;
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gamesIsPaused = true;
    }

    public void MainMenuScene()
    {
        SceneManager.LoadScene(0);
        Resume();
    }

    public void QuitScene()
    {
        Application.Quit();
    }
}

