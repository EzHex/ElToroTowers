using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PowerUpTower : UtilityTower
{
    [SerializeField] private float speedBoost;
    [SerializeField] private float rangeBoost;
    [SerializeField] private float damageBoost;

    public float GetRangeBoost()
    {
        return rangeBoost;
    }

    public float GetDamageBoost()
    {
        return damageBoost;
    }

    public float GetSpeedBoost()
    {
        return speedBoost;
    }

    public override void TilesFeature()
    {
        List<Tile> affectedTiles = GetAffectedTiles();
        foreach (var item in affectedTiles.Where(item => item != null && !item.GetIsPath()))
        {
            item.SetAffection(this);
            Tower tower = item.GetComponentInChildren<Tower>();
            if (tower != null) tower.Init(rangeBoost, speedBoost, damageBoost);
        }
    }

    public override void DestroyTower()
    {
        Destroy(gameObject);
        List<Tile> affectedTiles = GetAffectedTiles();
        foreach (var item in affectedTiles.Where(item => item != null && !item.GetIsPath()))
        {
            item.RemoveAffection(this);
            //item.SetOriginalColor();
            Tower tower = item.GetComponentInChildren<Tower>();
            if (tower != null) tower.Init(-rangeBoost, -speedBoost, -damageBoost);
        }
    }

    public void CheckRangeOnObstacleDestroy()
    {
        List<Tile> affectedTiles = GetAffectedTiles();

        var list = affectedTiles.Select(item => item).Where(item => item.GetCurrentColor() == GetColor()).ToList();

        if (list.Count > 0)
        {
            foreach (var item in affectedTiles.Where(item => item != null && !item.GetIsPath() && !item.GetIsObstacle()))
            {
                if (item.GetCurrentColor() != GetColor())
                {
                    item.ChangeTileColor(GetColor());
                }
            }
        }
    }

    public override void ToggleRange()
    {
        List<Tile> affectedTiles = GetAffectedTiles();
        Tile tile = GetComponentInParent<Tile>();

        foreach (var item in affectedTiles.Where(item =>
            item != null && !item.GetIsPath() && !item.GetIsObstacle() && this == item.GetAffector()))
        {
            if (item.GetCurrentColor() != GetColor())
            {
                item.ChangeTileColor(GetColor());
            }
            else
            {
                item.SetOriginalColor();
            }
        }
    }
}
