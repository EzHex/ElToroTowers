using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TowerUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private ITower tower;

    [SerializeField] private GameObject rButton;
    [SerializeField] private GameObject dButton;
    [SerializeField] private GameObject sButton;

    [SerializeField] private TMP_Text rPriceLabel;
    [SerializeField] private TMP_Text dPriceLabel;
    [SerializeField] private TMP_Text sPriceLabel;

    [SerializeField] private float rangeDefaultUpgradePrice;
    [SerializeField] private float damageDefaultUpgradePrice;
    [SerializeField] private float speedDefaultUpgradePrice;

    [SerializeField] private float rangeUpgradePrice;
    [SerializeField] private float damageUpgradePrice;
    [SerializeField] private float speedUpgradePrice;

    [SerializeField] private float rangeUpgrade;
    [SerializeField] private float damageUpgrade;
    [SerializeField] private float speedUpgrade;

    private PlayerStats playerStats;

    void Awake()
    {
        canvas.SetActive(false);
        playerStats = FindObjectOfType<PlayerStats>();

        rPriceLabel.text = rangeUpgradePrice.ToString();
        dPriceLabel.text = damageUpgradePrice.ToString();
        sPriceLabel.text = speedUpgradePrice.ToString();
    }

    public void TowerUIOn()
    {
        canvas.SetActive(true);
    }

    public void TowerUIOff()
    {
        canvas.SetActive(false);
    }

    public void DisableUpgrades()
    {
        rButton.SetActive(false);
        dButton.SetActive(false);
        sButton.SetActive(false);
    }

    public void SellTower()
    {
        tower = GetComponentInChildren<ITower>();
        playerStats.IncreaseMoney(tower.GetSellPrice());
        var tile = GetComponent<Tile>();
        tile.DestroyTower();
        tower.DestroyTower();
    }

    public void UpgradeTowerSpeed()
    {
        if (speedUpgradePrice <= playerStats.GetMoney())
        {
            playerStats.ReduceMoney(speedUpgradePrice);
            Tower t = GetComponentInChildren<Tower>();
            t.Init(speedUpgrade, 0, 0);
            t.IncreaseSellPrice((int)(speedUpgradePrice * 0.8f));
            speedUpgradePrice = (int)(speedUpgradePrice * 1.5f);
            sPriceLabel.text = speedUpgradePrice.ToString();
        }
    }

    public void UpgradeTowerRange()
    {
        if (rangeUpgradePrice <= playerStats.GetMoney())
        {
            playerStats.ReduceMoney(rangeUpgradePrice);
            Tower t = GetComponentInChildren<Tower>();
            t.Init(1, rangeUpgrade, 0);
            t.IncreaseSellPrice((int)(rangeUpgradePrice * 0.8f));
            rangeUpgradePrice = (int)(rangeUpgradePrice * 1.5f);
            rPriceLabel.text = rangeUpgradePrice.ToString();
        }
    }

    public void UpgradeTowerDamage()
    {
        if (damageUpgradePrice <= playerStats.GetMoney())
        {
            playerStats.ReduceMoney(damageUpgradePrice);
            Tower t = GetComponentInChildren<Tower>();
            t.Init(1, 0, damageUpgrade);
            t.IncreaseSellPrice((int)(damageUpgradePrice * 0.8f));
            damageUpgradePrice = (int)(damageUpgradePrice * 1.5f);
            dPriceLabel.text = damageUpgradePrice.ToString();
        }
    }

    public void OnTowerDestroy()
    {
        rangeUpgradePrice = rangeDefaultUpgradePrice;
        damageUpgradePrice = damageDefaultUpgradePrice;
        speedUpgradePrice = speedDefaultUpgradePrice;

        rPriceLabel.text = rangeUpgradePrice.ToString();
        dPriceLabel.text = damageUpgradePrice.ToString();
        sPriceLabel.text = speedUpgradePrice.ToString();
    }
}
