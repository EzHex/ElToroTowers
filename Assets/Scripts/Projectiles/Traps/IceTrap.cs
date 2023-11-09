using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrap : Trap
{
    public override void OnCollisionWithEnemy(Enemy enemy)
    {
        enemy.TakeDamage(GetDamage());
        enemy.SpeedMultiply(0.8f);
    }
    public override void OnExitWithEnemy(Enemy enemy)
    {
        enemy.SpeedMultiply(1.25f);
    }
}
