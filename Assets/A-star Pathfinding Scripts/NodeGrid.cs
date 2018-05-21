using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// This is a grid of nodes for the A* engine
/// </summary>
public class NodeGrid : MonoBehaviour {

    List<Node> path = new List<Node>();

    public Vector2Int gridWorldSize;
    float nodeDiameter; //Based on the tilemap, assumes that all the cells are squarish
    public Tilemap floorTilemap;
    Node[,] grid;

    public bool shouldGizmo;

    int gridSizeX, gridSizeY;

    public void Initialize()
    {
        if (gridWorldSize.x % 2 != 0 || gridWorldSize.y % 2 != 0) Debug.LogError("The grid dimesnions should be even");
        nodeDiameter = floorTilemap.cellSize.x;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter); //Given that the node diameter it'd default of 1, this shoudl cause no trouble at all
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        generateGrid();
    }

    void OnDrawGizmos()
    {
        if (!shouldGizmo) return;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null)
        {
            foreach (Node node in grid)
            {
                Gizmos.color = (node.walkable) ? Color.green : Color.red;
                if (path != null && path.Contains(node)) Gizmos.color = Color.yellow;
                Gizmos.DrawCube(node.worldPosition, Vector3.one * (nodeDiameter - .1f)); //.1 was subtracted for aesthetics 
            }
        }
    }

    void generateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                bool walkable = (floorTilemap.HasTile(new Vector3Int(x - gridSizeX/2, y - gridSizeY/2, 0)));
                Node node = new Node(walkable, new Vector3Int(x - gridSizeX / 2, y - gridSizeY/2, 0), floorTilemap, x, y);
                grid[x, y] = node;
            }
        }
    }


    //Based off the assumption that the grid and dungeon have their origin at Vector2.zero
    public Node nodeFromWorldPos(Vector2 pos)
    {
        float percentX = (pos.x / floorTilemap.cellSize.x + gridSizeX / 2) / gridSizeX; //Extreme left yeilds 0, extreme right yields 1;
        float percentY = (pos.y / floorTilemap.cellSize.y + gridSizeY / 2) / gridSizeY;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY); //Just to prevent errors anyone leaves the dungeon bounds

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

        return grid[x, y];
    }

    public List<Node> getNeighbours (Node node)
    {
        List<Node> neighbours = new List<Node>();
        for (int x = -1; x < 2; x++)
        {
            for (int y = -1; y < 2; y++)
            {
                if (x == 0 && y == 0) continue; //Skip the nose itslef
                if (x != 0 && y != 0) continue; //Skip diagonals too

                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                //Checking if this neighbour is in the grid in the first place
                if ((checkX >= 0 && checkX < gridSizeX) && (checkY >= 0 && checkY < gridSizeY))
                {
                    neighbours.Add(grid[checkX, checkY]); 
                }
            }
        }
        return neighbours;
    }
}
