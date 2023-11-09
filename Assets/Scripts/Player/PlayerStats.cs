using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float XP;
    private float targetXP;
    private int playerLevel;
    private int score;
    private int highScore;
    private string highScoreKey = "HighScore";
    [SerializeField] private Image XPBar;

    [SerializeField] private float money;

    [SerializeField] private int lives;

    void Start()
    {
        XP = 0;
        targetXP = 83;
        playerLevel = 1;
        score = 0;
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        XPBar.fillAmount = 0;
    }

    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(highScoreKey, highScore);
            PlayerPrefs.Save();
        }
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
    }

    public void ReduceMoney(float amount)
    {
        money -= amount;
    }

    public void IncreaseMoney(float amount)
    {
        money += amount;
    }

    public void ReduceLives(int damage)
    {
        if (lives > 0)
            lives -= damage;
    }

    public void IncreaseXP(int amount)
    {
        XP += amount;

        while (XP >= targetXP)
        {
            XP -= targetXP;
            targetXP *= 1.104f;
            playerLevel++;
        }

        XPBar.fillAmount = XP / targetXP;
    }

    public int GetPlayerLevel()
    {
        return playerLevel;
    }

    public int GetScore()
    {
        return score;
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public float GetMoney()
    {
        return money;
    }

    public int GetLives()
    {
        return lives;
    }
}
