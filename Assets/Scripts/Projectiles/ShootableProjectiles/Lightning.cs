using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Lightning : Projectile
{
    [SerializeField] private GameObject lightning;
    [SerializeField] private int _bounceCount;
    [SerializeField] private float _bounceRadius;
    [SerializeField] private List<Enemy> hittedEnemies = new List<Enemy>();
    [SerializeField] private Enemy haveToHit;

    public override void OnTriggerEnter2D(Collider2D other)
    {
        // If gameObject that we colided is an enemy
        if (other.gameObject.tag == "Enemy")
        {
            //Out of the collider we hit, we take the enemy
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            //Give him damage
            enemy.TakeDamage(GetDamage());
            //Add the enemy to a list where we track who got fucked
            hittedEnemies.Add(enemy);
            //Reduce bounce count
            _bounceCount--;
            Enemy nextToHit = GetClosestEnemyThatWasntHit(enemy.transform.position,enemy, _bounceRadius, hittedEnemies);
            haveToHit = nextToHit;
            if(nextToHit == null)
            {
                Destroy(gameObject);
                return;
            }
            SetAttackLocation(nextToHit.transform);
            if(_bounceCount < 0)
            {
                Destroy(gameObject);
                return;
            }
        }    
    }
    public Enemy GetClosestEnemyThatWasntHit(Vector3 position,Enemy from, float maxRange, List<Enemy> hittedEnemies)
    {
        List<GameObject> enemyList = new List<GameObject>();
        enemyList.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));
        Enemy closest = null;
        foreach (GameObject gameObject in enemyList)
        {
            Enemy enemy = gameObject.GetComponent<Enemy>();
            if(enemy == from)
            {
                continue;
            }
            if (enemy.GetHealth() <= 0) continue;
            if (Vector3.Distance(position, enemy.transform.position) <= maxRange && !hittedEnemies.Contains(enemy))
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
} 
