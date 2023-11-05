using System.Collections.Generic;
using UnityEngine;
public class PathManager : MonoBehaviour
{
    public static PathManager Instance;
    private TileNode destinationTile;
       
       // ... (your existing fields)
   
     
       void Awake()
       {
           Instance = this;
       }
       
   public List<TileNode> FindPath(TileNode startNode, TileNode destinationNode)
   {
       // Initialize open and closed sets
       List<TileNode> openSet = new List<TileNode>();
       HashSet<TileNode> closedSet = new HashSet<TileNode>();
   
       // Add the start node to the open set
       openSet.Add(startNode);
   
       while (openSet.Count > 0)
       {
           // Find the node in the open set with the lowest fCost
           TileNode currentNode = openSet[0];
           for (int i = 1; i < openSet.Count; i++)
           {
               if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
               {
                   currentNode = openSet[i];
               }
           }
   
           // Remove the current node from the open set and add it to the closed set
           openSet.Remove(currentNode);
           closedSet.Add(currentNode);
   
           // Check if we've reached the destination
           if (currentNode == destinationNode)
           {
               List<TileNode> path = RetracePath(startNode, destinationNode);
   
               if (path != null)
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
   
           // Explore neighboring nodes
           List<TileNode> neighbors = GetNeighbors(currentNode);
   
           foreach (TileNode neighbor in neighbors)
           {
               if (closedSet.Contains(neighbor) || neighbor.hasPassenger)
               {
                   continue;
               }
   
               int newMovementCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
   
               if (newMovementCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
               {
                   neighbor.gCost = newMovementCostToNeighbor;
                   neighbor.hCost = GetDistance(neighbor, destinationNode);
                   neighbor.parent = currentNode;
   
                   if (!openSet.Contains(neighbor))
                   {
                       openSet.Add(neighbor);
                   }
               }
           }
       }
   
       // No path found
       Debug.Log("No path found.");
       return null;
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
   
   private int GetDistance(TileNode nodeA, TileNode nodeB)
   {
       // Implement your distance calculation here (e.g., Manhattan distance, Euclidean distance)
       // Return the cost of moving from nodeA to nodeB
       return Mathf.Abs(nodeA.tileId - nodeB.tileId);
   }
   
   private List<TileNode> RetracePath(TileNode startNode, TileNode endNode)
   {
       List<TileNode> path = new List<TileNode>();
       TileNode currentNode = endNode;
   
       while (currentNode != startNode)
       {
           path.Add(currentNode);
           currentNode = currentNode.parent;
       }
   
       path.Reverse();
       return path;
   }
}
