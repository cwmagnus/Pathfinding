using UnityEngine;

/// <summary>
/// Actual tile object in the scene.
/// </summary>
public class TileObject : MonoBehaviour
{
    public Vector2Int Position { get; private set; }
    public bool Walkable { get; private set; }
    public int Value { get; private set; }

    /// <summary>
    /// Generate the tile object from the given tile.
    /// </summary>
    /// <param name="tile">Tile to render.</param>
    /// <param name="row">Location in row.</param>
    /// <param name="column">Location in column.</param>
    public void Generate(Tile tile, int row, int column)
    {
        Position = new Vector2Int(row, column);
        Walkable = tile.Walkable;
        Value = tile.Value;

        // Generate the sprite renderer
        SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = tile.Sprite;
    }
}