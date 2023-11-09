using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueTrap : Trap
{
    public override void OnCollisionWithEnemy(Enemy enemy)
    {
        enemy.SpeedMultiply(0.5f);
    }
    public override void OnExitWithEnemy(Enemy enemy)
    {
        enemy.SpeedMultiply(2f);
    }
}
