using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingNeighbourNodes : MonoBehaviour
{
    public static CheckingNeighbourNodes Instance;
     private TileNode destinationTile;
    
  
    void Awake()
    {
        Instance = this;
    }
    public List<TileNode> FindPath(TileNode startNode, TileNode destinationNode)
    {
        List<TileNode> path = new List<TileNode>();
        if (ExplorePath(startNode, destinationNode, path))
        {
            Debug.Log("Path Found! Tile IDs in the path:");
            foreach (TileNode node in path)
            {
                Debug.Log(node.tileId);
            }
            return path;
        }
        else
        {
            Debug.Log("No valid path found.");
            return null;
        }
    }

    private bool ExplorePath(TileNode currentNode, TileNode destinationNode, List<TileNode> path)
    {
        // Add the current node to the path
        path.Add(currentNode);

        // Check if we've reached the destination
        if (currentNode == destinationNode)
        {
            return true;
        }

        List<TileNode> neighbors = GetNeighbors(currentNode);

        foreach (TileNode neighbor in neighbors)
        {
            if (neighbor != null && !path.Contains(neighbor) && !neighbor.hasPassenger)
            {
                // Recursively explore the neighbor
                if (ExplorePath(neighbor, destinationNode, path))
                {
                    return true;
                }
            }
        }

        // If no valid path is found from this node, remove it from the path
        path.Remove(currentNode);
        return false;
    }
    private List<TileNode> GetNeighbors(TileNode node)
    {
        List<TileNode> neighbors = new List<TileNode>();
        if (node.rightNode != null)
        {
            neighbors.Add(node.rightNode);
        }
        if (node.upNode != null)
        {
            neighbors.Add(node.upNode);
        }
        if (node.backNode != null)
        {
            neighbors.Add(node.backNode);
        }
        if (node.leftNode != null)
        {
            neighbors.Add(node.leftNode);
        }
        return neighbors;
    }

}
