using UnityEngine;

/// <summary>
/// Node on the path finding graph.
/// </summary>
public class Node
{
    public Node Parent { get; private set; }
    public Vector2Int Position { get; private set; }

    public float G { get; set; }
    public float H { get; set; }
    public float F { get; set; }

    /// <summary>
    /// Create a new node.
    /// </summary>
    /// <param name="parent">Parent of this node.</param>
    /// <param name="position">Position of this node on the tile map.</param>
    public Node(Node parent, Vector2Int position)
    {
        Parent = parent;
        Position = position;
    }

    /// <summary>
    /// Check if this node is equal to another node.
    /// </summary>
    /// <param name="other">Node to check against.</param>
    /// <returns>True if positions on the tile map are equal.</returns>
    public bool Equals(Node other)
    {
        return Position == other.Position;
    }
}