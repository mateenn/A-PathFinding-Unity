
using UnityEngine;

public class TileNode : MonoBehaviour
{
    public int tileId;
    public bool hasPassenger;
    public TileNode rightNode, upNode, backNode, leftNode;
    
    public int gCost;  // The cost of moving from the start node to this node
    public int hCost;  // The estimated cost from this node to the destination node
    public int fCost { get { return gCost + hCost; } }  // The total cost
    public TileNode parent;  // The parent node in the path


}
