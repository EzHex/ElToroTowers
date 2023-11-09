using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour, ITower
{
    [SerializeField] private float range;
    [SerializeField] private GameObject projectile;

    [SerializeField] private float price;
    [SerializeField] private float sellPrice;

    [SerializeField] private float rangeModifier = 0;
    [SerializeField] private float projSpeedModifier = 0;
    [SerializeField] private float damageModifier = 0;
    [SerializeField] private float shootingSpeed;
    private float nextShot = 0;

    [SerializeField] private Circle circle;

    private void Awake()
    {
        circle = GetComponent<Circle>();
        sellPrice = (int)(price * 0.8f);
    }

    public virtual void Update()
    {
        if (Time.time > nextShot)
        {
            Enemy enemy = GetClosestEnemy(transform.position, range + rangeModifier);
            if (enemy != null)
            {
                GameObject ar = Instantiate(projectile, transform.position, Quaternion.identity);
                Projectile proj = ar.GetComponent<Projectile>();
                proj.Init(enemy.transform, projSpeedModifier, damageModifier);
                nextShot = Time.time + shootingSpeed;
            }
        }
    }

    public void Init(float shootingSpeedMod = 1, float rangeMod = 0, float damageMod = 0)
    {
        shootingSpeed *= shootingSpeedMod;
        rangeModifier += rangeMod;
        damageModifier += damageMod;
        circle.Refresh();
    }

    public float GetPrice()
    {
        return price;
    }
    public float GetSellPrice()
    {
        return sellPrice;
    }

    public void IncreaseSellPrice(float inc)
    {
        sellPrice += inc;
    }
    public float GetShootingSpeed()
    {
        return shootingSpeed;
    }

    public float GetRange()
    {
        return range + rangeModifier;
    }
    public GameObject getProjectile()
    {
        return projectile;
    }

    //TODO
    //Physics2D.OverlapSphere
    public Enemy GetClosestEnemy(Vector3 position, float maxRange)
    {
        List<GameObject> enemyList = new List<GameObject>();
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Enemy closest = null;
        foreach (GameObject gameObject in enemyList)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            if (enemy.GetHealth() <= 0) continue;
            if (Vector3.Distance(position, enemy.transform.position) <= maxRange)
            {
                if (closest == null)
                {
                    closest = enemy;
                }
                else
                {
                    if (Vector3.Distance(position, enemy.transform.position) < Vector3.Distance(position, closest.transform.position))
                    {
                        closest = enemy;
                    }
                }
            }
        }
        return closest;
    }

    public void DestroyTower()
    {
        TowerUI twUI = GetComponentInParent<TowerUI>();
        twUI.OnTowerDestroy();
        Destroy(gameObject);
    }

    public void ToggleRange()
    {
        circle.ToggleRange();
    }
}
