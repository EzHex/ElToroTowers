using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _imageRot;
    [SerializeField] private float _Angle;
    [SerializeField] private Transform attackLocation;

    [SerializeField] private float speedModifier;
    [SerializeField] private float damageModifier;

    public virtual void Update()
    {
        if (attackLocation == null)
        {
            Destroy(gameObject);
        }
        else
        {
            Vector3 moveDir = (transform.position - attackLocation.position).normalized; // Move direction for calculating angles
            transform.position = Vector3.MoveTowards(transform.position, attackLocation.position, (_speed + speedModifier) * Time.deltaTime);
            float angle = GetAngleFromVectorFloat(moveDir);
            transform.eulerAngles = new Vector3(0, 0, angle - _imageRot);
        }
    }

    public float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += _Angle*2;
        n += _Angle;
        return n;
    }
    public void Init(Transform attackLoc, float speedMod = 0, float damageMod = 0)
    {
        attackLocation = attackLoc;
        speedModifier = speedMod;
        damageModifier = damageMod;
    }
    public Transform GetAttackLocation()
    {
        return attackLocation;
    }
    public void SetAttackLocation(Transform attackLoc)
    {
        attackLocation = attackLoc;
    }

    public float GetDamage()
    {
        return _damage + damageModifier;
    }
    public float GetImageRotation()
    {
        return _imageRot;
    }
    public float GetAngle()
    {
        return _Angle;
    }
    public float GetSpeed()
    {
        return _speed;
    }

    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(_damage + damageModifier);
            }
        }
    }
}
