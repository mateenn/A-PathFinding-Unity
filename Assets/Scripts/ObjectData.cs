using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObjectData : MonoBehaviour
{
    public Variables.ObjectType ObjectType;
    public TileNode myTile;
    public TileNode destinationTile;
    public List<TileNode> path;
    public int currentWaypoint;
    private bool _isToMovePlayer;
    public void PerformPassengerAction()
    {
        Debug.Log("Performing Actions");
        path = PathManager.Instance.FindPath(myTile, destinationTile);
        if (path != null)
        {
            _isToMovePlayer = true;
            myTile.hasPassenger = false;
        }
        
        //PathManager.Instance.SearchNode(myTile);
    }
    private void Update()
    {
        if(!_isToMovePlayer) return;
        if (path != null && currentWaypoint < path.Count)
        {
            // Move the player towards the current waypoint
            Transform playerTransform = transform;
            TileNode waypoint = path[currentWaypoint];
            Vector3 targetPosition = new Vector3(waypoint.transform.position.x, playerTransform.position.y, waypoint.transform.position.z);
            float moveSpeed = 5.0f; // Adjust the speed as needed

            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

            // Check if the player has reached the current waypoint
            if (playerTransform.position == targetPosition)
            {
                currentWaypoint++;
            }
        }
    }

    
}
