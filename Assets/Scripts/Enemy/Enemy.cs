using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    private float startHealth;
    [SerializeField] private int awardedXP;
    [SerializeField] private int awardedPoints;
    [SerializeField] private int awardedMoney;
    [SerializeField] private Image healthBar;
    [SerializeField] private float movementSpeed;
    private PlayerStats playerStats;
    [SerializeField] private GameObject blood;

    void Start()
    {
        startHealth = health;
        playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0)
        {
            Die();
        }
    }
    public float GetSpeed()
    {
        return movementSpeed;
    }
    public void SpeedMultiply(float multiplier)
    {
        movementSpeed *= multiplier;
    }

    void Die()
    {
        playerStats.IncreaseXP(awardedXP);
        playerStats.IncreaseScore(awardedPoints);
        playerStats.IncreaseMoney(awardedMoney);
        Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return health;
    }
}
