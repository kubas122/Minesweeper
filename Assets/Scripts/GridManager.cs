using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;
    public GameObject tilePrefab;
    public int rows = 8;
    public int columns = 8;
    public int mineCount = 10;

    private GameObject[,] grid;
    private bool gameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        GenerateGrid();
        PlaceMines();
        AdjustCamera();
    }

    public void GenerateGrid()
    {
        grid = new GameObject[rows, columns];
        float tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float spacing = 0.1f;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector3 position = new Vector3(col * (tileSize + spacing), -row * (tileSize + spacing), 0);
                GameObject tile = Instantiate(tilePrefab, position, Quaternion.identity, transform);
                tile.name = $"Tile_{row}_{col}";

                Tile tileScript = tile.GetComponent<Tile>();
                tileScript.row = row;
                tileScript.column = col;

                grid[row, col] = tile;
            }
        }
    }

    public void PlaceMines()
    {
        int placedMines = 0;

        while (placedMines < mineCount)
        {
            int randomRow = Random.Range(0, rows);
            int randomCol = Random.Range(0, columns);

            Tile tileScript = grid[randomRow, randomCol].GetComponent<Tile>();

            if (!tileScript.isMine)
            {
                tileScript.isMine = true;
                placedMines++;
            }
        }
    }

    public void RevealTile(int row, int column)
    {
        if (gameOver) return;

        Tile tileScript = grid[row, column].GetComponent<Tile>();

        if (!tileScript.isRevealed)
        {
            tileScript.Reveal();
            GameManager.Instance.AddScore(1);
        }
    }

    public void GameOver()
    {
        gameOver = true;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Tile tileScript = grid[row, col].GetComponent<Tile>();
                if (tileScript.isMine)
                {
                    tileScript.RevealMine();
                }
            }
        }

        Debug.Log("Przegrana!");
    }

    void AdjustCamera()
    {
        Camera mainCamera = Camera.main;

        float tileSize = tilePrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        float spacing = 0.1f;
        float gridWidth = columns * (tileSize + spacing);
        float gridHeight = rows * (tileSize + spacing);

        mainCamera.transform.position = new Vector3(gridWidth / 2f - tileSize / 2f, -gridHeight / 2f + tileSize / 2f, -10f);

        float cameraSize = Mathf.Max(gridWidth / mainCamera.aspect, gridHeight) / 2f;
        mainCamera.orthographicSize = cameraSize + 0.5f;
    }

}
