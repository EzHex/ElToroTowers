using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private Text moneyText;
    [SerializeField] private PlayerStats playerStats;
 
    void Update()
    {
        moneyText.text = "$" + playerStats.GetMoney();
    }
}
