using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Projectile
{
    [SerializeField] private Vector3 startPoint;
    private float startTime;
    [SerializeField] private Vector3 endPoint;
    private Vector3 center;
    [SerializeField] private float centerOffset;
    [SerializeField] private float rotationSpeed;
    private bool reachedEnd = false;

    private void Start()
    {
        startTime = Time.time;
        startPoint = transform.position;
    }
    public override void Update()
    {
        if (GetAttackLocation() == null)
        {
            if (reachedEnd)
            {
                if(Vector2.Distance(transform.position,endPoint) < 0.1)
                {
                    Destroy(gameObject);
                }

            }
            if (!reachedEnd)
            {
                startTime = Time.time;
                reachedEnd = true;
                var swap = endPoint;
                endPoint = startPoint;
                startPoint = swap;

            }
        }

        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);

        center = (startPoint + endPoint) * 0.5f;
        //While the end wasn't reached
        if (!reachedEnd)
        {
            //Endpoint is Attack Location (Updating it)
            endPoint = GetAttackLocation().position;
            center -= new Vector3(0, centerOffset, 0);
        }
        else
            center += new Vector3(0, 2*centerOffset, 0);

        Vector3 riseRelCenter = startPoint - center;
        Vector3 setRelCenter = endPoint - center;

        float fracComplete = (Time.time - startTime) / GetSpeed();

        transform.position = Vector3.Slerp(riseRelCenter, setRelCenter, fracComplete);
        transform.position += center;

        if(transform.position == endPoint)
        {
            if (reachedEnd)
            {
                Destroy(gameObject);
            }
            startTime = Time.time;
            reachedEnd = true;
            var swap = endPoint;
            endPoint = startPoint;
            startPoint = swap;
        }
    }
}
