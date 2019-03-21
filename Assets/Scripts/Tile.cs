using UnityEngine;

/// <summary>
/// Holds information about a tile.
/// </summary>
[CreateAssetMenu(menuName = "Custom/Tile")]
public class Tile : ScriptableObject
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private bool walkable;
    [SerializeField] private int value;

    public Sprite Sprite { get { return sprite; } }
    public bool Walkable { get { return walkable; } }
    public int Value { get { return value; } }
}
