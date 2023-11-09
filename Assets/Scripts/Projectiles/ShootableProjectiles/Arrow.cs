using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private bool hasHit = false; // Initialize Hit boolean so we have only a single collision

    // We do trigger in this class cause different Projectiles give out different damage outputs
    // This case - Arrow 15
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null && !hasHit)
            {
                enemy.TakeDamage(GetDamage());
                hasHit = true;
            }
            Destroy(gameObject);
        }
    }
}
