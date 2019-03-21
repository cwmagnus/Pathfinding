using UnityEngine;

/// <summary>
/// Handles generating the world of tiles from a simple map.
/// </summary>
public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private Tile[] tiles;
    [SerializeField] private int tileSize;
    [SerializeField] private GameObject tileObjectPrefab;

    public TileObject[][] GeneratedMap { get; private set; }

    // Simple hard coded example map
    // NOTE: Number is equal to the index of the tiles array
    private readonly int[][] map = new int[][] 
    { 
        new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int [] { 0, 0, 1, 0, 0, 1, 1, 0, 0, 0 },
        new int [] { 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 },
        new int [] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
        new int [] { 0, 0, 0, 0, 2, 0, 1, 0, 0, 0 },
        new int [] { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
        new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int [] { 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
        new int [] { 0, 0, 1, 1, 1, 1, 1, 1, 0, 0 },
        new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
        new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
    };

    /// <summary>
    /// Start the world generation process.
    /// </summary>
    private void Awake()
    {
        Generate(map);

        // Fix world transform to offset tiles
        transform.rotation = Quaternion.Euler(0, 0, -90);
        transform.position = new Vector3((-map.Length * tileSize / 3f) - (tileSize / 2f), 
            (map[0].Length * tileSize / 2f) + (tileSize / 2f), 0);
    }

    /// <summary>
    /// Generate the map of tiles.
    /// </summary>
    /// <param name="map">Map of numbers to generate into tiles.</param>
    private void Generate(int[][] map)
    {
        GeneratedMap = new TileObject[map.Length][];

        for (int i = 0; i < map.Length; i++)
        {
            GeneratedMap[i] = new TileObject[map[i].Length];

            for (int j = 0; j < map[i].Length; j++)
            {
                // Create the tile object
                GameObject tileObject = Instantiate(tileObjectPrefab, new Vector2(i * tileSize,
                    j * tileSize), Quaternion.Euler(0, 0, 90),  transform) as GameObject;
                GeneratedMap[i][j] = tileObject.GetComponent<TileObject>();
                GeneratedMap[i][j].Generate(tiles[map[i][j]], i, j);
            }
        }
    }
}
