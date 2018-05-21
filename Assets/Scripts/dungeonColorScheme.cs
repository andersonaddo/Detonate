using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The values for difficulty levels (such as spawn rates) are held in these scriptable Objects
/// </summary>
[CreateAssetMenu(fileName = "New Dungeon Color Scheme")]
public class dungeonColorScheme : ScriptableObject
{
    public Sprite mainFloorTile, wallTile, secondaryFloorTile, tertiaryFloorTile;
    public Color backgroundColor;
}
