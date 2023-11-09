using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : Projectile
{
    //[SerializeField] private Collider2D collider;
    public void Start()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public override void Update()
    {
        if (GetAttackLocation() == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 moveDir = (transform.position - GetAttackLocation().position); // Move direction for calculating angles
            transform.position = Vector3.MoveTowards(transform.position, GetAttackLocation().position, GetSpeed() * Time.deltaTime);
            float angle = base.GetAngleFromVectorFloat(moveDir);
            transform.eulerAngles = new Vector3(0, 0, angle - GetImageRotation());
            if (transform.position == GetAttackLocation().position)
            {
                GetComponent<Collider2D>().enabled = true;
                StartCoroutine(waitForDamage());
            }
        }
    }
    IEnumerator waitForDamage()
    {
        yield return new WaitForSeconds(0.04f);
        Destroy(gameObject);
    }

}
