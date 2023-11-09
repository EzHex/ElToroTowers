using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleUI : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private float obstacleRemovePrice;
    [SerializeField] private Tile tile;

    // Start is called before the first frame update
    void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
        canvas.SetActive(false);
        tile = GetComponent<Tile>();
    }

    public void ObstacleUIOn()
    {
        canvas.SetActive(true);
    }
    
    public void ObstacleUIOff()
    {
        canvas.SetActive(false);
    }

    public void DestroyObstacle()
    {
        if (playerStats.GetMoney() >= obstacleRemovePrice)
        {
            playerStats.ReduceMoney(obstacleRemovePrice);
            tile.DestroyObstacle();
            if (tile.GetAffectedStatus())
            {
                var aff = tile.GetAffector();
                aff.CheckRangeOnObstacleDestroy();
            }
        }
    }
}
