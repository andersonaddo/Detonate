using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FloorGenerator : MonoBehaviour {

    public Tilemap boundaryTilemap, floorTilemap, wallTilemap;
    public Tile floorTile;

    [Tooltip("The probabilities that the generator will turn. Should add up to 100")]
    public int leftTurnChance, rightTurnChance, backwardsChance;
    public int turnChance; //Should also be out of 100

    public int lifeTime; //How long the generator will survive (in terms of tiles)
    int currentLife;
    public bool canGenerate = true;

    Vector3Int currentPosition = new Vector3Int();

    public NodeGrid pathfindingGrid;

    public enum directions
    {
        left,
        right,
        up,
        down
    }

    directions currentDirection;


    // Use this for initialization
    void Start () {
        currentDirection = (directions)Random.Range(0, 4);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 50; i++)
        {
            if (currentLife == lifeTime) return;
            if (canGenerate)
            {
                dropTile();
                turnIfNeeded();
                moveForward();
            }
        }
    }

    void dropTile()
    {
        if (floorTilemap.HasTile(currentPosition)) return;
        currentLife++;
        floorTilemap.SetTile(currentPosition, floorTile);
        if (currentLife == lifeTime)
        {
            //This means that we're done, and this method will never be called again.
            wallTilemap.GetComponent<generateWalls>().generateAllWalls();
            pathfindingGrid.Initialize();
            boundaryTilemap.gameObject.SetActive(false);
        }
    }

    //This is also used by the generateWalls class
    public static Vector3Int getVectorIncrement(directions direction)
    {
        switch (direction)
        {
            case directions.left:
                return Vector3Int.left;
            case directions.right:
                return Vector3Int.right;
            case directions.up:
                return Vector3Int.up;
            case directions.down:
                return Vector3Int.down;
            default:
                Debug.LogError("For some reason, this default clause was met! FIx this please");
                return Vector3Int.up;
        }
    }

    void turnIfNeeded()
    {
        //Decide if you will turn, or just move forward
        //Right or left turns are all relative to the current direction of the generator
        if (Random.Range(1, 101) <= turnChance)
        {
            bool hasTurned = false;

            int turnTypeChance = Random.Range(1, 101);
            int chanceIncrememnter = leftTurnChance;

            if (turnTypeChance <= chanceIncrememnter)
            {
                turnLeft();
                hasTurned = true;
            }

            chanceIncrememnter += rightTurnChance;
            if (turnTypeChance <= chanceIncrememnter && !hasTurned)
            {
                turnRight();
                hasTurned = true;
            }

            if (!hasTurned) turnAround();
        }

        turnIfMetBoundary();
    }

    bool turnIfMetBoundary()
    {
        //The boundary tle isn't blocking the way
        if (!boundaryTilemap.HasTile(currentPosition + getVectorIncrement(currentDirection)))
            return false;

        //First finding all unblocked areas
        List<directions> allDirections = new List<directions>(new directions[] { directions.up, directions.down, directions.left, directions.right });
        List<directions> availableDirections = new List<directions>();

        foreach (directions direction in allDirections)
        {
            if (!boundaryTilemap.HasTile(currentPosition + getVectorIncrement(direction)))
                availableDirections.Add(direction);
        }

        //Picking any one of those free directions to turn to
        currentDirection = availableDirections[Random.Range(0, availableDirections.Count)];
        return true;

    }

    void moveForward()
    {
        currentPosition = currentPosition + getVectorIncrement(currentDirection);
    }

    void turnAround()
    {
        switch (currentDirection)
        {
            case directions.left:
                currentDirection = directions.right;
                break;
            case directions.up:
                currentDirection = directions.down;
                break;
            case directions.right:
                currentDirection = directions.left;
                break;
            case directions.down:
                currentDirection = directions.up;
                break;
            default:
                Debug.LogError("For some reason, this default clause was met! FIx this please");
                break;
        }
    }

    void turnLeft()
    {
        switch (currentDirection)
        {
            case directions.left:
                currentDirection = directions.down;
                break;
            case directions.up:
                currentDirection = directions.left;
                break;
            case directions.right:
                currentDirection = directions.up;
                break;
            case directions.down:
                currentDirection = directions.right;
                break;
            default:
                Debug.LogError("For some reason, this default clause was met! FIx this please");
                break;
        }
    }

    void turnRight()
    {
        switch (currentDirection)
        {
            case directions.left:
                currentDirection = directions.up;
                break;
            case directions.up:
                currentDirection = directions.right;
                break;
            case directions.right:
                currentDirection = directions.down;
                break;
            case directions.down:
                currentDirection = directions.left;
                break;
            default:
                Debug.LogError("For some reason, this default clause was met! FIx this please");
                break;
        }
    }
}
