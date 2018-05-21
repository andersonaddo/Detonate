using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class generateEnemiesAndItems : MonoBehaviour {

    public Tilemap floorTilemap;
    Tilemap myTilemap;
    public Tile enemiyTile, itemTile, bombTile;
    public int numberOfBombs;

    public GameObject naziSpawner, lavaSpewer, circleHurler, healthPack, bomb;
    public int lavaChance, circleChance;

    public Vector3Int spawnPoint;
    public int minBombDistance;

    //Both of these should be add up to 100% or lesss
    public int itemChance, enemyChance;

    public PathFinder pathfinder;

    //Called by the wallgenerator class
    public void generateAllItems()
    {

        floorTilemap.CompressBounds();
        generateBombs();

        //Iterating through all the positions in the bounds of the floor tilemap and adding walls where needed
        foreach (var position in floorTilemap.cellBounds.allPositionsWithin)
        {
            if (!floorTilemap.HasTile(position)) continue;
            if (myTilemap.HasTile(position)) continue;

            int dropSeed = Random.Range(1, 101);
            int chanceIncrememnter = itemChance;

            if (dropSeed <= chanceIncrememnter)
            {
                dropItem(position);
                continue;
            }

            chanceIncrememnter += enemyChance;
            if (dropSeed <= chanceIncrememnter)
            {
                dropEnemy(position);
            }
        }

        myTilemap.gameObject.SetActive(false);
        pathfinder.enable();

    }

    void Start()
    {
        myTilemap = GetComponent<Tilemap>();
    }

    //Pick all the tiles far enough from the starting poisiton (taken to be the (0,0) tile) and place random tiles there
    void generateBombs()
    {
        List<Vector3Int> goodTiles = new List<Vector3Int>();
        foreach (var position in floorTilemap.cellBounds.allPositionsWithin)
        {
            if (!floorTilemap.HasTile(position)) continue;
            if (Mathf.CeilToInt(Vector3Int.Distance(position, spawnPoint)) < minBombDistance) continue;
            goodTiles.Add(position);
        }

        Debug.Log("The number of viable tiles for the bomb is: " + goodTiles.Count);

        for (int i = 0; i < numberOfBombs; i++)
        {
            Vector3Int chosenPosition = goodTiles[Random.Range(0, goodTiles.Count)];
            //Checking if a bomb has been placed here already
            if (myTilemap.HasTile(chosenPosition))
            {
                i--;
                continue;
            }
            myTilemap.SetTile(chosenPosition, bombTile);
            dropBomb(chosenPosition);
        }
    }

    void dropEnemy(Vector3Int pos)
    {
        myTilemap.SetTile(pos, enemiyTile);
        Vector3 rawPosition = myTilemap.CellToWorld(pos);
        rawPosition.x += myTilemap.cellSize.x / 2;
        rawPosition.y += myTilemap.cellSize.y / 2;
        Instantiate(chooseEnemy(), rawPosition, Quaternion.identity);
    }


    void dropItem(Vector3Int pos)
    {
        myTilemap.SetTile(pos, itemTile);
        Vector3 rawPosition = myTilemap.CellToWorld(pos);
        rawPosition.x += myTilemap.cellSize.x / 2;
        rawPosition.y += myTilemap.cellSize.y / 2;
        Instantiate(healthPack, rawPosition, Quaternion.identity);
    }

    void dropBomb(Vector3Int pos)
    {
        myTilemap.SetTile(pos, itemTile);
        Vector3 rawPosition = myTilemap.CellToWorld(pos);
        rawPosition.x += myTilemap.cellSize.x / 2;
        rawPosition.y += myTilemap.cellSize.y / 2;
        GameObject newBomb = Instantiate(bomb, rawPosition, Quaternion.identity);
        gameManager.Instance.updateBombsLeft(1);
        gameManager.Instance.bombs.Add(newBomb.GetComponent<bomb>());
    }

    GameObject chooseEnemy()
    {
        int seed = Random.Range(1, 101);

        int chanceIncrememnter = circleChance;

        if (seed <= chanceIncrememnter) return circleHurler;

        chanceIncrememnter += lavaChance;
        if (seed <= chanceIncrememnter) return lavaSpewer;

        return naziSpawner;
    }
}
