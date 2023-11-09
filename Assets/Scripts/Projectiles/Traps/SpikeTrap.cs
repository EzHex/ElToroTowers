using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    public override void OnCollisionWithEnemy(Enemy enemy)
    {
        enemy.TakeDamage(GetDamage());
    }
    public override void OnExitWithEnemy(Enemy enemy)
    {
    }
}
