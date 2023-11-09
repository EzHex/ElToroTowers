using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private PlayerStats playerStats;

    void Update()
    {
        scoreText.text = playerStats.GetScore().ToString();
    }
}
