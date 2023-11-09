using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private Sprite up;
    [SerializeField] public Sprite down;
    [SerializeField] public Sprite left;
    [SerializeField] public Sprite right;
    public SpriteRenderer render;
    private GridManager gridManager;   
    [SerializeField] private GameObject gridManagerObject;
    private PlayerStats playerStats;

    private Queue<Vector2> path;
    private Vector2 targetLocation;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;

    [SerializeField] private int DamageToNexus;


    private void Start()
    {
        render = this.gameObject.GetComponent<SpriteRenderer>();
        playerStats = GameObject.Find("GameMaster").GetComponent<PlayerStats>();
    }

    private void Awake()
    {
        gridManagerObject = GameObject.FindGameObjectWithTag("GridManager");
        gridManager = gridManagerObject.GetComponent<GridManager>();
        path = gridManager.GetPathCoordinates();
        targetLocation = path.Dequeue();
    }

    private void Update()
    {
        float step = GetComponent<Enemy>().GetSpeed() * Time.deltaTime;
       
        if (transform.position == (Vector3)targetLocation)
        {
            UpdateTarget();
        }
        Vector3 a = new Vector3();
        a = Vector3.MoveTowards(transform.position, targetLocation, step) - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, step);
        a.Normalize();
        
        //Debug.Log(a.x);
        if (a.x > 0.9)
        {
            //animator.SetFloat("Horizontal", 1);
            render.sprite = right;
            //Debug.Log("desine");
        }
        else if (a.x < -0.9)
        {
            //animator.SetFloat("Horizontal", -1);
            render.sprite = left;
            //Debug.Log("kita desine");
        }
        else if (a.y > 0.9)
        {
            //animator.SetFloat("Vertical", 1);
            render.sprite = up;
            //Debug.Log("lubos");

        }
        else if (a.y < -0.9)
        {
            //animator.SetFloat("Vertical", -1);
            render.sprite = down;
            //Debug.Log("grindys");
            //Debug.Log(render.sprite);
        }

        //animator.setFloat("Horizontal", path.x);
        //animator.setFloat("Vertical", path.y);
        //animator.setFloat("Speed", path.sqrMagnitude);
    }

    private void UpdateTarget()
    {
        if (path.Count != 0)
        {
            targetLocation = path.Dequeue();
        }
        else
        {
            playerStats.ReduceLives(DamageToNexus);
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
