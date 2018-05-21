using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Node {

    public bool walkable;
    public Vector2 worldPosition;
    public Vector3Int tilemapPosition;

    public int gridX, gridY;

    /// <summary>
    /// Distance from the starting pos
    /// </summary>
    public int gCost;

    /// <summary>
    /// Distance from the target pos
    /// </summary>
    public int hCost;
    
    /// <summary>
    /// The node that points to this wone and fives it the smallest fcost found
    /// </summary>
    public Node parent;

    public Node(bool walkable, Vector3Int tilemapPosition, Tilemap tilemap, int gridX, int gridY)
    {
        this.walkable = walkable;
        this.tilemapPosition = tilemapPosition;
        worldPosition = tilemap.CellToWorld(tilemapPosition);
        worldPosition.x += 0.5f * tilemap.cellSize.x;
        worldPosition.y += 0.5f * tilemap.cellSize.y;

        this.gridX = gridX;
        this.gridY = gridY;
    }

    public int fCost
    {
        get
        {
            return gCost + hCost;
        }
    }
}
