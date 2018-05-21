using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class generateWalls : MonoBehaviour {

    public Tilemap floorTilemap;
    Tilemap wallTilemap;
    public Tile wallTile;
    public generateEnemiesAndItems itemGenerator;

    List<FloorGenerator.directions> allDirections = new List<FloorGenerator.directions>(new FloorGenerator.directions[] {
        FloorGenerator.directions.up,
        FloorGenerator.directions.down,
        FloorGenerator.directions.left,
        FloorGenerator.directions.right });


    //Called by the floor generator class
    public void generateAllWalls () {

        floorTilemap.CompressBounds(); //Look up https://gamedev.stackexchange.com/questions/150917/how-to-get-all-tiles-from-a-tilemap
        BoundsInt bounds = floorTilemap.cellBounds;

        int numberChecked = 0;

        //Iterating through all the positions in the bounds of the floor tilemap and adding walls where needed
        foreach (var position in floorTilemap.cellBounds.allPositionsWithin)
        {
            if (!floorTilemap.HasTile(position)) continue;

            numberChecked++;

            foreach (FloorGenerator.directions direction in allDirections)
            {
                Vector3Int testPosition = position + FloorGenerator.getVectorIncrement(direction);
                if (floorTilemap.HasTile(testPosition)) continue;
                if (wallTilemap.HasTile(testPosition)) continue;
                wallTilemap.SetTile(testPosition, wallTile);
            }
        }

        wallTilemap.gameObject.SetActive(false);
        wallTilemap.gameObject.SetActive(true);

        itemGenerator.generateAllItems();
    }

    void Start () {
        wallTilemap = GetComponent<Tilemap>();
	}
}
