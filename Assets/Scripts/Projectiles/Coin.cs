using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float value;
    [SerializeField] private float existenceTime;
    private Transform target;
    private float moveSpeed;

    void Start()
    {
        StartCoroutine(SelfDestruct(existenceTime));
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }

    public float GetValue()
    {
        return value;
    }
    
    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    IEnumerator SelfDestruct(float existenceTime)
    {
        yield return new WaitForSeconds(existenceTime);
        Destroy(gameObject);
    }
}
