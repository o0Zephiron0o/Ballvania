using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Works with MovingPlatform.cs

//  Assign this to parent waypoint empty
//  * Have some child waypoint!

//  Function:
//  > Get waypoint index (of children from the empty)
//  > Manage which waypoint to go to next

//  Note:
//  > Remember waypointIndex starts at 0, not 1
//  > Having index of a child that doesn't exist will not work

public class WaypointPath : MonoBehaviour
{
    public Transform GetWaypoint(int waypointIndex)   // All this does is grab the Transform gameobject according to the index
    {
        return transform.GetChild(waypointIndex);
    }

    public int GetNextWaypointIndex(int currentWaypointIndex)   // All this does is add 1 to the index or loop it if it reach the max child count
    {
        int nextWaypointIndex = currentWaypointIndex + 1;

        if (nextWaypointIndex == transform.childCount)
        {
            nextWaypointIndex = 0;
        }

        return nextWaypointIndex;
    }

    private void OnDrawGizmos()
    {
        for (int waypointIndex = 0; waypointIndex < transform.childCount; waypointIndex++)
        {
            var waypoint = GetWaypoint(waypointIndex);

            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(waypoint.position, 0.2f);

            int nextWaypointIndex = GetNextWaypointIndex(waypointIndex);
            var nextWaypoint = GetWaypoint(nextWaypointIndex);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(waypoint.position, nextWaypoint.position);
        }
    }
}
