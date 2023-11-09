using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTower : Tower
{
    private GridManager gridManager;
    [SerializeField] private GameObject gridManagerObject;
    private List<Vector2> path = new List<Vector2>();
    private float nextShot = 0;
    // Start is called before the first frame update
    public void Start()
    {
        gridManagerObject = GameObject.FindGameObjectWithTag("GridManager");
        gridManager = gridManagerObject.GetComponent<GridManager>();
        Queue<Vector2> tiles = gridManager.GetPathCoordinates();
        updatePath(tiles, GetRange());
    }

    // Update is called once per frame
    public override void Update()
    {
        //if (Input.GetKeyDown("up"))
        if(Time.time > nextShot)
        {
            if (path.Count != 0)
            {
                int random = Random.Range(0, path.Count - 1);
                GameObject child = Instantiate(getProjectile(), transform.position, Quaternion.identity);
                Tile tileInPosition = gridManager.GetTileAtPosition(path[random]);
                child.transform.parent = tileInPosition.transform;
                Projectile proj = child.GetComponent<Projectile>();
                proj.Init(tileInPosition.transform);
                nextShot = Time.time + GetShootingSpeed();
            }
        }
    }
    private void updatePath(Queue<Vector2> data, float maxRange)
    {
        List<Vector2> updated = new List<Vector2>();
        while (data.Count != 0)
        {
            Vector2 get = data.Dequeue();
            if (Vector2.Distance(get, this.transform.position) <= maxRange)
            {
                path.Add(get);
            }
        }
    }
}
