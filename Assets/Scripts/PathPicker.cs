using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// User controlled path picker.
/// </summary>
public class PathPicker : MonoBehaviour
{
    [SerializeField] private WorldGenerator worldGenerator;
    [SerializeField] private int numberOfMoves;
    private Vector2Int? startNodePosition;
    private Pathfinder pathfinder = new Pathfinder();

    /// <summary>
    /// Get the start node position.
    /// </summary>
    private void Start()
    {
        startNodePosition = FindStartPosition();
    }

    /// <summary>
    /// Pick and highlight the path.
    /// </summary>
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Clear the tiles
            ClearDisplayedTiles(worldGenerator.GeneratedMap);

            Vector2Int? endNodePosition = GetMouseOverTilePosition();
            if (startNodePosition != null && endNodePosition != null)
            {
                // Find path of nodes
                TileObject[][] tileMap = worldGenerator.GeneratedMap;
                List<Node> nodes = pathfinder.FindPath(tileMap, 
                    startNodePosition.Value, endNodePosition.Value, numberOfMoves);
                if (nodes != null)
                {
                    foreach (Node node in nodes)
                    {
                        // Display nodes that are not the starting node
                        if (node.Position != startNodePosition.Value)
                        {
                            TileObject tile = tileMap[node.Position.x][node.Position.y];
                            DisplayTile(tile, (int)node.G);
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// Finds the position in the generated map of the starting position.
    /// </summary>
    /// <returns>Position of the start node or null if none is found.</returns>
    private Vector2Int? FindStartPosition()
    {
        for (int i = 0; i < worldGenerator.GeneratedMap.Length; i++)
        {
            for (int j = 0; j < worldGenerator.GeneratedMap[i].Length; j++)
            {
                if (worldGenerator.GeneratedMap[i][j].Value == 2) return worldGenerator.GeneratedMap[i][j].Position;
            }
        }
        return null;
    }

    /// <summary>
    /// Get the position of the node the mouse is hovering over.
    /// </summary>
    /// <returns>Position of the node under the mouse or null if there is nothing under the mouse.</returns>
    private Vector2Int? GetMouseOverTilePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
        if (hit.collider != null)
        {
            TileObject tileObject = hit.transform.GetComponent<TileObject>();
            if (tileObject != null)
            {
                return tileObject.Position;
            }
        }
        return null;
    }

    /// <summary>
    /// Display the tile in a path.
    /// </summary>
    /// <param name="tile">Tile to display.</param>
    /// <param name="nodeCost">Cost of the node.</param>
    private void DisplayTile(TileObject tile, int nodeCost)
    {
        tile.GetComponent<SpriteRenderer>().color = Color.red;
        tile.GetComponentInChildren<Text>().text = nodeCost.ToString();
    }

    /// <summary>
    /// Clear all tiles of any colors or text.
    /// </summary>
    /// <param name="tileMap">Tile map to clear.</param>
    private void ClearDisplayedTiles(TileObject[][] tileMap)
    {
        for (int i = 0; i < tileMap.Length; i++)
        {
            for (int j = 0; j < tileMap[i].Length; j++)
            {
                TileObject tile = tileMap[i][j];
                tile.GetComponent<SpriteRenderer>().color = Color.white;
                tile.GetComponentInChildren<Text>().text = string.Empty;
            }
        }
    }
}
