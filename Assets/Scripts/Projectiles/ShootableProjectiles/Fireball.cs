using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Projectile
{
    public override void Update()
    {
        if (GetAttackLocation() == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 moveDir = (transform.position - GetAttackLocation().position); // Move direction for calculating angles
            transform.position = Vector2.MoveTowards(transform.position, GetAttackLocation().position, GetSpeed() * Time.deltaTime);
            float angle = base.GetAngleFromVectorFloat(moveDir);
            transform.eulerAngles = new Vector3(0, 0, angle - GetImageRotation());
            if(transform.position == GetAttackLocation().position)
            {
                Destroy(gameObject);
            }
        }
    }
}
