using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    [SerializeField] private Text livesText;
    [SerializeField] private PlayerStats playerStats;

    void Update()
    {
        livesText.text = playerStats.GetLives().ToString();
    }
}
