using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UtilityTower : MonoBehaviour, ITower
{
    [SerializeField] private List<Tile> affectedTiles = new List<Tile>();
    [SerializeField] protected int affectionCount; // 4(left top right bottom) or 8(left left-top top top-right right right-bottom bottom bottom-left)
    [SerializeField] private float price;
    [SerializeField] private Color affectionColor;

    private GridManager gridManager;

    private void Awake()
    {
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        FindAffectedTiles();
        TilesFeature();
    }

    public void FindAffectedTiles()
    {
        Vector2 pos = transform.position;
        switch (affectionCount)
        {
            case 4:
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x - 1, pos.y)));    // left
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x + 1, pos.y)));    // right
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x, pos.y + 1)));    // top
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x, pos.y - 1)));    // bottom
                break;
            case 8:
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x - 1, pos.y)));    // left
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x + 1, pos.y)));    // right
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x, pos.y + 1)));    // top
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x, pos.y - 1)));    // bottom

                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x - 1, pos.y - 1)));// bottom - left
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x + 1, pos.y + 1)));// top - right    
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x - 1, pos.y + 1)));// top - left
                affectedTiles.Add(gridManager.GetTileAtPosition(new Vector2(pos.x + 1, pos.y - 1)));// bottom - right
                break;
            default:
                return;
        }
    }

    public List<Tile> GetAffectedTiles()
    {
        return affectedTiles;
    }

    public float GetPrice()
    {
        return price;
    }

    public Color GetColor()
    {
        return affectionColor;
    }

    public virtual void TilesFeature()
    {
        
    }

    public virtual void DestroyTower()
    {
        Destroy(gameObject);
    }

    public virtual void ToggleRange()
    {
        
    }

    public float GetSellPrice()
    {
        return price * 0.8f;
    }
}
