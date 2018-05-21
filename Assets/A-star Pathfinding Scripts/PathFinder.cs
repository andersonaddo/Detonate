using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder : MonoBehaviour {

    NodeGrid grid;
    bool canGenerate = false;
    public Transform playerTransform;

    public void enable()
    {
        canGenerate = true;
    }

    public bool isEnabled()
    {
        return canGenerate;
    }

    void Start()
    {
        grid = GetComponent<NodeGrid>();
    }

    public List<Node> FindPathToPlayer(Vector3 startPos)
    {
        Node startNode = grid.nodeFromWorldPos(startPos);
        Node endNode = grid.nodeFromWorldPos(playerTransform.position);

        List<Node> openNodes = new List<Node>();
        HashSet<Node> closedNodes = new HashSet<Node>();
        openNodes.Add(startNode);

        while (openNodes.Count > 0)
        {
            Node currentNode = openNodes[0];

            //Getting the node with the smallest fcost. This is actually pretty expensive to be doing repeatedly
            for (int i = 1; i < openNodes.Count; i++)
            {
                if (currentNode.fCost > openNodes[i].fCost || (currentNode.fCost == openNodes[i].fCost && currentNode.hCost > openNodes[i].hCost))
                {
                    currentNode = openNodes[i];
                }
            }
            openNodes.Remove(currentNode);
            closedNodes.Add(currentNode);

            //We found the target!
            if (currentNode == endNode)
            {
                List<Node> path = retracePath(startNode, endNode);
                return path;
            }

            foreach (Node neighbour in grid.getNeighbours(currentNode))
            {
                if (!neighbour.walkable || closedNodes.Contains(neighbour)) continue;

                int newMovementCostToNeighbour = currentNode.gCost + getDistance(currentNode, neighbour);
                if (newMovementCostToNeighbour < neighbour.gCost /*Meaning we've found a shorter path to thid neighbour*/ || !openNodes.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = getDistance(neighbour, endNode);
                    neighbour.parent = currentNode;

                    if (!openNodes.Contains(neighbour)) openNodes.Add(neighbour);
                }
            }
        }
        return null;
        
    }

    //This method doesn't use pythagoras theory to get distnace. Refer to
    // https://www.youtube.com/watch?v=mZfyt03LDH4&t=14m
    int getDistance(Node nodeA, Node nodeB)
    {
        int xDistance = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int yDistance = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if (xDistance > yDistance) return (14 * yDistance + 10 * (xDistance - yDistance));
        return (14 * xDistance + 10 * (yDistance - xDistance));
    }

    List<Node> retracePath(Node startNode, Node endNode)
    {
        List <Node> pathOfNoes = new List<Node>();
        Node currentNode = endNode;

        while (currentNode != startNode)
        {
            pathOfNoes.Add(currentNode);
            currentNode = currentNode.parent;
        }

        pathOfNoes.Reverse();
        return pathOfNoes;

    }
}
