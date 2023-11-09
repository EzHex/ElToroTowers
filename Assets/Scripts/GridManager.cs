using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class GridManager : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;

    [SerializeField] private Tile tile;
    [SerializeField] private new Transform camera;
    [SerializeField] private GameObject tilesParent;
    [SerializeField] private double chanceToSpawnObstacle;


    private Dictionary<Vector2, Tile> tiles;

    private Dictionary<Vector2, int> tilesMatrix;

    private List<Vector2> enemyPath = new List<Vector2>();

    private SetupManager setupManager;

    private List<Vector2> neighbours = new List<Vector2>();

    private void Awake()
    {
        setupManager = FindObjectOfType<SetupManager>();
        GenerateGrid();
        if (setupManager != null)
        {
            int map = setupManager.GetMap();

            switch (map)
            {
                case 1:
                    SetupMap();
                    break;
                case 2:
                    SetupPointMap(setupManager.GetPointMapTurns());
                    break;
                case 3:
                    SetupMapDijkstra();
                    break;
                default:
                    break;
            }
        }
        else
        {
            SetupMapDijkstra();
        }

        GenerateObstacles();
        GetPathNeighbouringTiles();
        GetBorders();
    }

    private void SetupMap()
    {

        tilesMatrix[new Vector2(0, 7)] = 1;
        enemyPath.Add(new Vector2(0, 7));
        tilesMatrix[new Vector2(1, 7)] = 1;
        enemyPath.Add(new Vector2(1, 7));
        tilesMatrix[new Vector2(2, 7)] = 1;
        enemyPath.Add(new Vector2(2, 7));
        tilesMatrix[new Vector2(2, 6)] = 1;
        enemyPath.Add(new Vector2(2, 6));
        tilesMatrix[new Vector2(2, 5)] = 1;
        enemyPath.Add(new Vector2(2, 5));
        tilesMatrix[new Vector2(2, 4)] = 1;
        enemyPath.Add(new Vector2(2, 4));
        tilesMatrix[new Vector2(2, 3)] = 1;
        enemyPath.Add(new Vector2(2, 3));
        tilesMatrix[new Vector2(2, 2)] = 1;
        enemyPath.Add(new Vector2(2, 2));
        tilesMatrix[new Vector2(3, 2)] = 1;
        enemyPath.Add(new Vector2(3, 2));
        tilesMatrix[new Vector2(4, 2)] = 1;
        enemyPath.Add(new Vector2(4, 2));
        tilesMatrix[new Vector2(5, 2)] = 1;
        enemyPath.Add(new Vector2(5, 2));
        tilesMatrix[new Vector2(6, 2)] = 1;
        enemyPath.Add(new Vector2(6, 2));
        tilesMatrix[new Vector2(7, 2)] = 1;
        enemyPath.Add(new Vector2(7, 2));
        tilesMatrix[new Vector2(8, 2)] = 1;
        enemyPath.Add(new Vector2(8, 2));
        tilesMatrix[new Vector2(9, 2)] = 1;
        enemyPath.Add(new Vector2(9, 2));
        tilesMatrix[new Vector2(9, 3)] = 1;
        enemyPath.Add(new Vector2(9, 3));
        tilesMatrix[new Vector2(9, 4)] = 1;
        enemyPath.Add(new Vector2(9, 4));
        tilesMatrix[new Vector2(10, 4)] = 1;
        enemyPath.Add(new Vector2(10, 4));
        tilesMatrix[new Vector2(11, 4)] = 1;
        enemyPath.Add(new Vector2(11, 4));
        tilesMatrix[new Vector2(12, 4)] = 1;
        enemyPath.Add(new Vector2(12, 4));
        tilesMatrix[new Vector2(13, 4)] = 1;
        enemyPath.Add(new Vector2(13, 4));
        tilesMatrix[new Vector2(14, 4)] = 1;
        enemyPath.Add(new Vector2(14, 4));
        tilesMatrix[new Vector2(15, 4)] = 1;
        enemyPath.Add(new Vector2(15, 4));
        
        foreach (var item in tilesMatrix.Where(item => item.Value == 1))
        {
            tiles[item.Key].SetPath();
        }
    }

    private void GenerateObstacles()
    {
        foreach (var item in tilesMatrix)
        {
            if (item.Value == 0)
            {
                if (Random.value >= (100-chanceToSpawnObstacle) / 100)
                {
                    tiles[item.Key].SetObstacle();
                }
            }
        }
        
    }

    private void Print(int[][] a)
    {
        foreach (var item in a)
        {
            foreach (var item2 in item)
            {
                Debug.Log(item2);
            }
        }
    }

    private void SetupPointMap(int turnsCount)
    {
        List<int> allX = new List<int>();
        while (true)
        {
            allX = CreateXList(turnsCount);
            string str = "";
            foreach (var item in allX)
            {
                str += item.ToString() + " ";
            }
            if (allX.Count == turnsCount) break;
        }

        List<Vector2> vecs = new List<Vector2>();
        for (int i = 0; i < allX.Count; i++)
        {
            vecs.Add(new Vector2(allX[i], Random.Range(0, height)));
        }

        for (int i = 1; i < vecs.Count; i++)
        {
            Vector2 start = vecs[i - 1];
            Vector2 end = vecs[i];

            for (int i2 = (int)start.x; i2 <= end.x; i2++)
            {
                Vector2 v = new Vector2(i2, start.y);
                tilesMatrix[v] = 1;
                enemyPath.Add(v);
            }

            if (start.y < end.y)
            {
                for (int i2 = (int)start.y; i2 < end.y; i2++)
                {
                    Vector2 v = new Vector2(end.x, i2);
                    tilesMatrix[v] = 1;
                    enemyPath.Add(v);
                }
            }
            else
            {
                for (int i2 = (int)start.y; i2 > end.y; i2--)
                {
                    Vector2 v = new Vector2(end.x, i2);
                    tilesMatrix[v] = 1;
                    enemyPath.Add(v);
                }
            }
        }

        foreach (var item in tilesMatrix.Where(item => item.Value == 1))
        {
            tiles[item.Key].SetPath();
        }
    }

    private List<int> CreateXList(int count)
    {
        List<int> result = new List<int> ();
        int x = 0;

        int diff = width / (count);

        for (int i = 0; i < count-1; i++)
        {
            result.Add(x);
            x += diff;
        }

        result.Add(width - 1);

        return result;
    }
    private void GetPathNeighbouringTiles()
    {
        Queue<Vector2> data = GetPathCoordinates();
        while (data.Count != 0)
        {
            Vector2 get = data.Dequeue();
            //Jei x ne 0, ieskome kaimyno kaireje
            if (get.x != 0)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x - 1, get.y));
                if (!neighbour.GetIsPath())
                {
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                    neighbour.ChangeSprite("E");
                }
            }
            if (get.x != 0 && get.y != 0)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x - 1, get.y - 1));
                if (!neighbour.GetIsPath())
                {
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                    neighbour.ChangeSprite("NE");
                }
            }
            if (get.x != 0 && get.y != height-1)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x - 1, get.y + 1));
                if (!neighbour.GetIsPath())
                {
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                    neighbour.ChangeSprite("SE");
                }
            }
            if (get.x != width-1 && get.y != 0)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x + 1, get.y - 1));
                if (!neighbour.GetIsPath())
                {
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                    neighbour.ChangeSprite("NW");
                }
            }
            if (get.x != width - 1 && get.y != height - 1)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x + 1, get.y + 1));
                if (!neighbour.GetIsPath())
                {
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                    neighbour.ChangeSprite("SW");
                }
            }
            // Jei y ne 0, ieskome kaimyno apacioje
            if (get.y != 0)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x, get.y - 1));

                if (!neighbour.GetIsPath())
                {
                    neighbour.ChangeSprite("N");
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                }
            }
            // Jei x ne maximalus width, ieskome kaimyno desineje
            if (get.x != width - 1)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x + 1, get.y));

                if (!neighbour.GetIsPath())
                {
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                    neighbour.ChangeSprite("W");
                }
            }
            // Jei y ne maximalus width, ieskome kaimyno virsuje
            if (get.y != height - 1)
            {
                Tile neighbour = GetTileAtPosition(new Vector2(get.x, get.y + 1));

                if (!neighbour.GetIsPath())
                {
                    //neighbour.ChangeTileColor(new Color(1, 0, 1, 1));
                    neighbour.ChangeSprite("S");
                }
            }

        }
    }
    private void GetBorders()
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height;y++)
            {
                Tile current = GetTileAtPosition(new Vector2(x,y));
                // If we are at the left side of the map, we need rocks on the left side
                if(x == 0)
                {
                    //current.ChangeTileColor(new Color(1, 0, 1, 1));
                    current.ChangeSprite("W");
                }
                // If we are at the bottom side of the map, we need rocks on the bottom side
                if (y == 0)
                {
                    //current.ChangeTileColor(new Color(1, 0, 1, 1));
                    current.ChangeSprite("S");
                }
                // If we are at the right side of the map, we need rocks on the right side
                if (x == width-1)
                {
                    //current.ChangeTileColor(new Color(1, 0, 1, 1));
                    current.ChangeSprite("E");
                }
                // If we are at the top side of the map, we need rocks on the top side
                if (y == height-1)
                {
                    //current.ChangeTileColor(new Color(1, 0, 1, 1));
                    current.ChangeSprite("N");
                }
            }
        }
    }

    public Vector2 GetStartPoint()
    {
        return enemyPath[0];
    }

    public Queue<Vector2> GetPathCoordinates()
    {
        Queue<Vector2> result = new Queue<Vector2>();
        foreach (var item in enemyPath)
        {
             result.Enqueue(item);
        }

        return result;
    }
    

    public void GenerateGrid()
    {
        tiles = new Dictionary<Vector2, Tile>();
        tilesMatrix = new Dictionary<Vector2, int>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var spawnedTile = Instantiate(tile, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";

                var isOffSet = (((x & 1) == 0) && ((y & 1) != 0)) || (((x & 1) != 0) && ((y & 1) == 0));
                spawnedTile.Init(isOffSet);

                tiles.Add(new Vector2(x, y), spawnedTile);
                tilesMatrix.Add(new Vector2(x, y), 0);

                spawnedTile.transform.parent = tilesParent.transform;
            }
        }

        camera.transform.position = new Vector3((float)width / 2 - 0.5f, (float)height / 2 - 0.5f, -10);
    }

    public Tile GetTileAtPosition(Vector2 pos)
    {
        return tiles.TryGetValue(pos, out var tile) ? tile : null;
    }
    private void SetupMapDijkstra()
    {
        List<int> path = new List<int>();
        int[] graphArray = new int[width * height];
        List<List<int>> adjacencyList = createAdjacencyArray(graphArray, width);
        for (int i = 0; i < 10000000; i++)
        {
            List<List<int>> weightList = weightListInitiate(adjacencyList);
            randomizeWeightMatrix(weightList, adjacencyList);
            path.Clear();
            int[] prec = new int[width * height];
            int[] distances = new int[width * height];
            int[][] adjancencyArray = new int[width * height][];
            int[][] weightArray = new int[width * height][];
            DoubleListToTwoDArray(adjancencyArray, adjacencyList);
            DoubleListToTwoDArray(weightArray, weightList);
            Dijkstra(adjancencyArray, weightArray, prec, distances, path, 112, 79);
            if (path.Count > 25)
                break;
        }
        path.Reverse();
        for (int i = 0; i < path.Count; i++)
        {
            int y = path[i] / width;
            int x = path[i] % width;
            tilesMatrix[new Vector2(x,y)] = 1;
            enemyPath.Add(new Vector2(x,y));
        }
        foreach (var item in tilesMatrix.Where(item => item.Value == 1))
        {
            tiles[item.Key].SetPath();
        }
    }
    private static void DoubleListToTwoDArray(int[][] result, List<List<int>> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            result[i] = data[i].ToArray();
        }
    }
    private static List<List<int>> weightListInitiate(List<List<int>> adjMatrix)
    {
        List<List<int>> weightList = new List<List<int>>();
        for (int i = 0; i < adjMatrix.Count; i++)
        {
            List<int> vs = new List<int>();
            for (int j = 0; j < adjMatrix[i].Count; j++)
            {
                vs.Add(-1);
            }
            weightList.Add(vs);
        }
        return weightList;
    }
    private static void randomizeWeightMatrix(List<List<int>> weightList, List<List<int>> adjMatrix)
    {
        int[] curr = new int[weightList.Count];
        for (int i = 0; i < curr.Length; i++)
        {
            curr[i] = 0;
        }
        for (int i = 0; i < weightList.Count; i++)
        {

            for (int j = 0; j < weightList[i].Count; j++)
            {
                if (weightList[i][j] == -1)
                {
                    weightList[i][j] = Random.Range(1, 100000);
                    if (weightList[adjMatrix[i][j]][curr[adjMatrix[i][j]]] == -1)
                    {
                        weightList[adjMatrix[i][j]][curr[adjMatrix[i][j]]] = weightList[i][j];
                        curr[adjMatrix[i][j]]++;
                    }
                }
            }
        }
    }
    private static List<List<int>> createAdjacencyArray(int[] graphArray, int width)
    {
        List<List<int>> adjacencyList = new List<List<int>>();
        for (int i = 0; i < graphArray.GetLength(0); i++)
        {
            List<int> neighbours = new List<int>();
            // Case where neighbour is above
            if (i >= width)
            {
                neighbours.Add(i - width);
            }
            //Case where neighbour is on the left
            if (i % width != 0)
            {
                neighbours.Add(i - 1);
            }
            //Case where neighbour is on the right
            if (i % width != width - 1)
            {
                neighbours.Add(i + 1);
            }
            //Case where the neighbour is below
            if ((graphArray.GetLength(0) - i) > width)
            {
                neighbours.Add(i + width);
            }
            adjacencyList.Add(neighbours);
        }
        return adjacencyList;
    }
    private static void Fill(int[] result,int variable)
    {
        for(int i = 0; i < result.Length; i++)
        {
            result[i] = variable;
        }
    }
    private static void Fill(bool[] result, bool variable)
    {
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = variable;
        }
    }
    private static void Dijkstra(int[][] graph, int[][] weight, int[] prec, int[] distances, List<int> Spath, int a, int b)
    {
        int pathEnd = b;
        Fill(distances, int.MaxValue);
        Fill(prec, 0);
        bool[] relaxed = new bool[prec.Length];
        Fill(relaxed, false);
        Queue<int> queue = new Queue<int>();
        int startingVertice = a;
        distances[startingVertice] = 0;
        relaxed[startingVertice] = true;
        prec[startingVertice] = startingVertice;
        queue.Enqueue(startingVertice);
        while (queue.Count != 0)
        {
            for (int i = 0; i < graph[queue.Peek()].Length; i++)
            {
                int vertice = graph[queue.Peek()][i];
                if (!relaxed[vertice] && ((distances[queue.Peek()] + weight[queue.Peek()][i]) < distances[vertice]))
                {
                    prec[vertice] = queue.Peek();
                    distances[vertice] = distances[queue.Peek()] + weight[queue.Peek()][i];
                }
            }
            int settledVertice = settle(relaxed, distances);
            if (settledVertice != -1)
            {
                relaxed[settledVertice] = true;
            }
            else
                break;
            queue.Enqueue(settledVertice);
            queue.Dequeue();
        }
        int pathGen = b;
        while (pathGen != a)
        {
            Spath.Add(pathGen);
            pathGen = prec[Spath[Spath.Count - 1]];
        }
        Spath.Add(a);
    }
    private static int settle(bool[] relaxed, int[] distances)
    {
        int dmin = int.MaxValue;
        int v = -1;
        for (int i = 0; i < distances.Length; i++)
        {
            if (!relaxed[i] && distances[i] < dmin)
            {
                dmin = distances[i];
                v = i;
            }
        }
        return v;
    }
}
