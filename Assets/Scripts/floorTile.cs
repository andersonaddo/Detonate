using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "floorTile")]
[System.Serializable]
public class floorTile : Tile
{

    
    Sprite chooseSprite()
    {
        int seed = Random.Range(0, 10);
        if (seed == 0) return sprite = gameManager.Instance.chosenScheme.secondaryFloorTile;

        if (seed == 1) return gameManager.Instance.chosenScheme.tertiaryFloorTile;

        return gameManager.Instance.chosenScheme.mainFloorTile;
    }

    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    {
        base.GetTileData(location, tileMap, ref tileData); 
            tileData.sprite = chooseSprite();
            tileData.colliderType = ColliderType.None;
    }
}
