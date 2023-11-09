using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MoneyTower : UtilityTower
{
    [SerializeField] private GameObject coin;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float firstThrow;
    [SerializeField] private float delay;
    [SerializeField] private SpawnEnemies spawnEnemies;
    private int currentWave;
    private bool isPressed;

    void Start()
    {
        spawnEnemies = GameObject.Find("Main Camera").GetComponent<SpawnEnemies>();
        currentWave = spawnEnemies.GetWave();
        isPressed = false;
        InvokeRepeating("ThrowCoin", firstThrow, delay);
    }

    void Update()
    {
        TilesFeature();
        if (isPressed)
        {
            List<Tile> affectedTiles = GetAffectedTiles();
            foreach (var tile in affectedTiles.Where(tile => tile != null && !tile.GetIsPath() && !tile.GetIsObstacle()))
            {
                if (tile.GetCurrentColor() != GetColor())
                {
                    tile.ChangeTileColor(GetColor());
                }
            }
        }
        
        if (currentWave < spawnEnemies.GetWave())
        {
            currentWave = spawnEnemies.GetWave();
            ThrowCoin();
        }
    }

    private void ThrowCoin()
    {
        List<Tile> affectedTiles = GetAffectedTiles();
        List<Tile> eligibleTiles = affectedTiles.Where(tile => tile != null && !tile.GetIsPath() && !tile.GetIsObstacle()).ToList();
        if (eligibleTiles.Count != 0)
        {
            int index = Random.Range(0, eligibleTiles.Count);
            GameObject obj = Instantiate(coin, transform.position, Quaternion.identity);
            obj.transform.parent = eligibleTiles[index].transform;
            Coin c = obj.GetComponent<Coin>();
            c.SetTarget(eligibleTiles[index].transform);
            c.SetMoveSpeed(moveSpeed);
            eligibleTiles[index].SetHasCoins(true);
        }
    }

    public override void TilesFeature()
    {
        List<Tile> affectedTiles = GetAffectedTiles();
        foreach (var tile in affectedTiles.Where(tile => tile != null && !tile.GetIsPath() && !tile.GetIsObstacle()))
        {
            tile.SetReserved(true);
        }
    }

    public override void DestroyTower()
    {
        Destroy(gameObject);
        List<Tile> affectedTiles = GetAffectedTiles();
        foreach (var tile in affectedTiles.Where(tile => tile != null && !tile.GetIsPath() && !tile.GetIsObstacle()))
        {
            tile.SetOriginalColor();
            tile.SetReserved(false);
        }
    }

    public override void ToggleRange()
    {
        List<Tile> affectedTiles = GetAffectedTiles();
        foreach (var tile in affectedTiles.Where(tile => tile != null && !tile.GetIsPath() && !tile.GetIsObstacle()))
        {
            if (tile.GetCurrentColor() != GetColor())
            {
                tile.ChangeTileColor(GetColor());
            }
            else
            {
                tile.SetOriginalColor();
            }
        }
        isPressed = !isPressed;
    }
}
