using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Finds a path on a tile map.
/// </summary>
public class Pathfinder
{
    /// <summary>
    /// Finds the path of nodes on the given map, from the start position to the end position.
    /// </summary>
    /// <param name="tileMap">Map to search for nodes on.</param>
    /// <param name="startPosition">Starting node position.</param>
    /// <param name="endPosition">End node position.</param>
    /// <param name="numberOfMoves">Number of moves that you can make to reach your destination.</param>
    /// <returns>The path of nodes or null if couldn't find a path.</returns>
    public List<Node> FindPath(TileObject[][] tileMap, Vector2Int startPosition, 
        Vector2Int endPosition, int numberOfMoves)
    {
        // Get your start and end nodes
        Node startNode = new Node(null, startPosition);
        startNode.G = startNode.H = startNode.F = 0;
        Node endNode = new Node(null, endPosition);
        endNode.G = endNode.H = endNode.F = 0;

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();

        openList.Add(startNode);

        // Keep searching while there are open nodes
        while (openList.Count > 0)
        {
            // Get the current node
            Node currentNode = openList[0];
            int currentIndex = 0;
            for (int i = 0; i < openList.Count; i++)
            {
                if (openList[i].F < currentNode.F)
                {
                    currentNode = openList[i];
                    currentIndex = i;
                }
            }

            // Close the current node
            openList.RemoveAt(currentIndex);
            closedList.Add(currentNode);

            // Don't return a path if there are no more moves
            if (currentNode.G > numberOfMoves)
            {
                Debug.Log("Not enough movement points");
                return null;
            }
                

            // Return the node path if we have reached our destination
            if (currentNode.Equals(endNode))
            {
                List<Node> path = new List<Node>();
                Node current = currentNode;
                while (current != null)
                {
                    path.Add(current);
                    current = current.Parent;
                }

                return path;
            }

            // Generate child nodes of the current node
            List<Node> children = new List<Node>();
            Vector2Int[] childrenPositions = new Vector2Int[]
            {
                new Vector2Int(0, 1),   // North
                new Vector2Int(1, 0),   // East
                new Vector2Int(0, -1),   // South
                new Vector2Int(-1, 0)    // West
            };
            foreach (Vector2Int position in childrenPositions)
            {
                Vector2Int nodePosition = new Vector2Int(currentNode.Position.x + position.x,
                    currentNode.Position.y + position.y);

                if (NotInTileMap(tileMap, nodePosition) || NotWalkable(tileMap, nodePosition))
                    continue;

                Node newNode = new Node(currentNode, nodePosition);

                children.Add(newNode);
            }

            // Add children not in the open list to it and skip if the child is clsoed
            foreach (Node child in children)
            {
                foreach (Node closedChild in closedList)
                {
                    if (child.Equals(closedChild))
                        continue;
                }

                child.G = currentNode.G + 1;
                child.H = Mathf.Pow(child.Position.x - endNode.Position.x, 2) +
                    Mathf.Pow(child.Position.y - endNode.Position.y, 2);
                child.F = child.G + child.H;

                foreach (Node openNode in openList)
                {
                    if (child.Equals(openNode) && child.G > openNode.G)
                        continue;
                }

                openList.Add(child);
            }
        }

        return null;
    }

    /// <summary>
    /// Checks if the position is not in the tile map.
    /// </summary>
    /// <param name="tileMap">Tile map to look in.</param>
    /// <param name="position">Position to check.</param>
    /// <returns>True if not in tile map.</returns>
    private bool NotInTileMap(TileObject[][] tileMap, Vector2Int position)
    {
        bool notInX = position.x > tileMap.Length - 1 || position.x < 0;
        bool notInY = position.y > tileMap[0].Length - 1 || position.y < 0;
        return notInX || notInY;
    }

    /// <summary>
    /// Checks if the tile is not walkable.
    /// </summary>
    /// <param name="tileMap">Tile map to look in.</param>
    /// <param name="posiiton">Position of the tile to check.</param>
    /// <returns>True if not walkable.</returns>
    private bool NotWalkable(TileObject[][] tileMap, Vector2Int posiiton)
    {
        return !tileMap[posiiton.x][posiiton.y].Walkable;
    }
}
