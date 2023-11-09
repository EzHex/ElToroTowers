using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Tile : MonoBehaviour
{
    [SerializeField] private Color baseColor;
    [SerializeField] private Color offSetColor;
    [SerializeField] private Color pathColor;
                     private Color originalColor;

    [SerializeField] private Sprite pathSprite;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private List<Sprite> baseSprites;

    [SerializeField] private new SpriteRenderer renderer;
    [SerializeField] private ShopUI shopUI;
    [SerializeField] private ObstacleUI obstacleUI;
    [SerializeField] private TowerUI towerUI;
    [SerializeField] private GlobalVariables globalVariables;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private SpriteMerger spriteMerger;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject obstacle;

    [SerializeField] private GameObject[] allTowers;
    [SerializeField] private List<GameObject> towers;

    [SerializeField] private int affectionCount;
    private GridManager gridManager;

    private bool isTower = false;
    private bool isObstacle = false;
    [SerializeField] private bool isPath = false;
    [SerializeField] private bool isAffected = false;
    private List<PowerUpTower> affectors = new List<PowerUpTower>();
    private bool isReserved = false;
    private bool hasCoins = false;

    private void Awake()
    {
        gridManager = GameObject.FindGameObjectWithTag("GridManager").GetComponent<GridManager>();
        globalVariables = FindObjectOfType<GlobalVariables>();
        SetupManager setupManager = FindObjectOfType<SetupManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        gameManager = FindObjectOfType<GameManager>();
        spriteMerger = FindObjectOfType<SpriteMerger>();
        renderer.sprite = baseSprite;

        if (setupManager != null)
        {
            List<int> selectedTowers = setupManager.GetSelectedTowers();
            int i2 = 0;
            foreach (int i in selectedTowers)
            {
                towers.Add(allTowers[i]);
                shopUI.SetupUI(allTowers[i].GetComponent<SpriteRenderer>(), i2);
                i2++;
            }
            return;
        }

        towers.Add(allTowers[1]);
        shopUI.SetupUI(allTowers[1].GetComponent<SpriteRenderer>(), 0);
        towers.Add(allTowers[2]);
        shopUI.SetupUI(allTowers[2].GetComponent<SpriteRenderer>(), 1);
        towers.Add(allTowers[3]);
        shopUI.SetupUI(allTowers[3].GetComponent<SpriteRenderer>(), 2);
        towers.Add(allTowers[4]);
        shopUI.SetupUI(allTowers[4].GetComponent<SpriteRenderer>(), 3);
    }
    public void ApplySpriteMerger(int number)
    {
        if (spriteMerger == null)
            return;
        
        var final = spriteMerger.getRender(renderer.sprite,number);
        ChangeSpriteRenderer(final);
    }

    public void Init(bool offSet)
    {
        renderer.color = offSet ? offSetColor : baseColor;
        originalColor = renderer.color;
    }

    public void ChangeTileColor(Color color)
    {
        renderer.color = color;
    }

    public bool GetAffectedStatus()
    {
        return isAffected;
    }

    public void ChangeSprite(string direction)
    {
        if (spriteMerger == null)
            return;
        
        spriteMerger.getBaseSprite(baseSprite);
        switch (direction)
        {
            case "N":
                ApplySpriteMerger(1);
                break;
            case "NE":
                ApplySpriteMerger(2);
                break;
            case "E":
                ApplySpriteMerger(3);
                break;
            case "SE":
                ApplySpriteMerger(4);
                break;
            case "S":
                ApplySpriteMerger(5);
                break;
            case "SW":
                ApplySpriteMerger(6);
                break;
            case "W":
                ApplySpriteMerger(7);
                break;
            case "NW":
                ApplySpriteMerger(8);
                break;
            default:
                break;

        }
        if(direction == "W")
        {
            ApplySpriteMerger(7);
        }
    }
    public void ChangeSpriteRenderer(SpriteRenderer spriteToChange)
    {
        renderer.sprite = spriteToChange.sprite;
    }

    public Color GetCurrentColor()
    {
        return renderer.color;
    }

    public void SetOriginalColor()
    {
        renderer.color = originalColor;
    }

    public void SetPath()
    {
        renderer.color = pathColor;
        renderer.sprite = pathSprite;
        isPath = true;
    }

    public bool GetIsPath()
    {
        return isPath;
    }

    public bool GetIsObstacle()
    {
        return isObstacle;
    }

    public void SetObstacle()
    {
        obstacle.SetActive(true);
        isObstacle = true;
    }

    public void DestroyObstacle()
    {
        obstacle.SetActive(false);
        isObstacle = false;
    }

    public void SetReserved(bool value)
    {
        isReserved = value;
    }

    public void SetHasCoins(bool value)
    {
        hasCoins = value;
    }

    public bool GetIsTower()
    {
        return isTower;
    }

    private void OnMouseEnter()
    {
        bool usingShop = globalVariables.GetShopState();
        bool inPauseMenu = globalVariables.GetPauseState();
        if (!isPath && !usingShop && !gameManager.getGameEnded() && !inPauseMenu && !isObstacle)
            highlight.SetActive(true);
    }

    private void OnMouseDown()
    {
        bool usingShop = globalVariables.GetShopState();
        bool inPauseMenu = globalVariables.GetPauseState();
        bool gameEnd = gameManager.getGameEnded();

        if (hasCoins && !usingShop && !gameEnd && !inPauseMenu)
        {
            Transform[] children = gameObject.GetComponentsInChildren<Transform>().Where(chd => chd.gameObject.tag == "Coin").ToArray();
            if (children.Length != 0)
            {
                foreach (Transform child in children)
                {
                    Coin c = child.gameObject.GetComponent<Coin>();
                    playerStats.IncreaseMoney(c.GetValue());
                    Destroy(child.gameObject);
                }
                hasCoins = false;
                return;
            }
        }

        if (!isTower && !isPath && !usingShop && !gameEnd && !inPauseMenu && !isObstacle && !isReserved)
        {
            shopUI.ShowShop();
            globalVariables.UsingShopTrue();
        }
        else if(isTower && !isPath && !usingShop && !gameEnd && !inPauseMenu && !isObstacle)
        {
            towerUI.TowerUIOn();
            globalVariables.UsingShopTrue();
        }
        else if(isObstacle && !isTower && !isPath && !usingShop && !gameEnd && !inPauseMenu)
        {
            obstacleUI.ObstacleUIOn();
            globalVariables.UsingShopTrue();
        }
    }

    public void ToggleRange()
    {
        var t = GetComponentInChildren<ITower>();
        t.ToggleRange();
    }

    public void ExitShopState()
    {
        globalVariables.UsingShopFalse();
    }

    public void SpawnTower(int i)
    {
        ITower t = towers[i].GetComponent<Tower>();
        if (t == null)
        {
            t = towers[i].GetComponent<UtilityTower>();
        }

        if (!t.GetType().Name.Equals("MoneyTower") && !isTower && !isPath)
        {
            if (t.GetPrice() <= playerStats.GetMoney())
            {
                if (t.GetType().IsSubclassOf(typeof(UtilityTower)))
                {
                    TowerUI towerUI = GetComponent<TowerUI>();
                    towerUI.DisableUpgrades();
                }

                playerStats.ReduceMoney(t.GetPrice());
                GameObject child = Instantiate(towers[i], transform.position, transform.rotation);
                child.transform.parent = transform;
                isTower = true;

                if (isAffected)
                {
                    child.TryGetComponent(out Tower tower);
                    if (tower != null) tower.Init(affectors[0].GetRangeBoost(), affectors[0].GetSpeedBoost(), affectors[0].GetDamageBoost());
                }

                shopUI.HideShop();
                ExitShopState();
            }
            else Debug.Log("Not enough gold");
        }
        else if (t.GetType().Name.Equals("MoneyTower") && !isTower && !isPath)
        {
            List<Tile> affectedTiles = new List<Tile>();
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
            if (affectedTiles.Where(tile => tile != null && tile.GetIsTower()).Count() == 0)
            {
                if (t.GetPrice() <= playerStats.GetMoney())
                {
                    TowerUI towerUI = GetComponent<TowerUI>();
                    towerUI.DisableUpgrades();
                    playerStats.ReduceMoney(t.GetPrice());
                    GameObject child = Instantiate(towers[i], transform.position, transform.rotation);
                    child.transform.parent = transform;
                    isTower = true;
                    shopUI.HideShop();
                    ExitShopState();
                }
                else Debug.Log("Not enough gold");
            }
            else
            {
                if (t.GetPrice() <= playerStats.GetMoney())
                {
                    Debug.Log("Inappropriate place");
                }
                else
                {
                    Debug.Log("Not enough gold");
                }
            }
        }
    }

    public void DestroyTower()
    {
        if (isTower)
            isTower = false;
    }

    public void SetAffection(PowerUpTower aff)
    {
        if ( aff != null )
        {
            isAffected = true;
            affectors.Add(aff);
            if (isTower)
            {
                Tower tower = GetComponentInChildren<Tower>();
                tower.Init(affectors[0].GetRangeBoost(), affectors[0].GetSpeedBoost(), affectors[0].GetDamageBoost());
            }
        }
    }

    public void RemoveAffection(PowerUpTower aff)
    {
        if (aff != null)
        {
            affectors.Remove(aff);
            CheckAffection();
        }
    }

    public PowerUpTower GetAffector()
    {
        return affectors[0];
    }

    public void CheckAffection()
    {
        if (affectors.Count == 0)
        {
            isAffected = false;
            SetOriginalColor();
        }
    }

    private void OnMouseExit()
    {
        bool inPauseMenu = globalVariables.GetPauseState();
        bool usingShop = globalVariables.GetShopState();
        if (!isPath && !usingShop && !gameManager.getGameEnded() && !inPauseMenu && !isObstacle)
            highlight.SetActive(false);
    }
}
