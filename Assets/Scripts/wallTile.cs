using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "wallTile")]
[System.Serializable]
public class wallTile : Tile
{

    public override void GetTileData(Vector3Int location, ITilemap tileMap, ref TileData tileData)
    {
        base.GetTileData(location, tileMap, ref tileData);
        tileData.sprite = gameManager.Instance.chosenScheme.wallTile;
    }
}