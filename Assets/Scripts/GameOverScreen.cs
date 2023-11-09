using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreText;
    [SerializeField] private PlayerStats playerStats;

    public void Setup()
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        highScoreText.text = "High Score: " + playerStats.GetHighScore();
        scoreText.text = "Score: " + playerStats.GetScore();
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Map1");
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
