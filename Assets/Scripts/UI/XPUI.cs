using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPUI : MonoBehaviour
{
    [SerializeField] private Text levelText;
    [SerializeField] private PlayerStats playerStats;

    void Update()
    {
        levelText.text = playerStats.GetPlayerLevel().ToString();
    }
}
