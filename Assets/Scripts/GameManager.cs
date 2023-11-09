using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;
    [SerializeField] private SpawnEnemies spawnEnemiesScript;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameOverScreen gameOverScreen;

    void Update()
    {
        if (gameEnded)
        {
            return;
        }
        if (playerStats.GetLives() <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverScreen.Setup();
        int currentWave = spawnEnemiesScript.GetWave();
        if (currentWave > PlayerPrefs.GetInt("wave")) PlayerPrefs.SetInt("wave", currentWave);
        Debug.Log("Game Over!");
    }

    public bool getGameEnded()
    {
        return gameEnded;
    }
}
