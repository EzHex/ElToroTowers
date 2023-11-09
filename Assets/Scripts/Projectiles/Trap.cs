using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Projectile
{
    [SerializeField] private int useCount;
    private int getUseCount()
    {
        return useCount;
    }
    public void decreaseUseCount()
    {
        useCount--;
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            OnCollisionWithEnemy(enemy);
            decreaseUseCount();
            if (getUseCount() == 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public virtual void OnCollisionWithEnemy(Enemy enemy)
    {
    }
    public virtual void OnExitWithEnemy(Enemy enemy)
    {
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            OnExitWithEnemy(enemy);
        }
    }
}
