using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  Works with WaypointPath.cs

//  Assign this to a Moving Platform gameobject

//  Function:
//  > Set previous and target waypoint
//  > Move platfrom from previous waypoint to target waypoint (transform)

//  Note:
//  > Numbers are 0 by default

public class MovingPlatform : MonoBehaviour
{
    [Header("Settings")] // Set in inspector!
    [SerializeField] private WaypointPath _waypointPath;
    [SerializeField] private float _speed;

    [Header("Current Waypoint")] // Don't need to touch in inspector, just for debug.
    [SerializeField] private int _waypointIndex;
    [SerializeField] private Transform _previousWaypoint;
    [SerializeField] private Transform _targetWaypoint;

    [Header("Elapsed Time")] // Don't need to touch in inspector, just for debug.
    [SerializeField] private float _timeToWaypoint;
    [SerializeField] private float _elapsedTime;

    //[Header("Object")]
    //[SerializeField] private Collider _objectOnPlatform;

    void Start()
    {
        TargetNextWaypoint();   // Set the previous Transform to the first
    }

    void Update() // Change to FixedUpdate
    {
        _elapsedTime += Time.deltaTime;
        //Debug.Log("Elapsed Time: " + _elapsedTime);

        float elapsedPecentage = _elapsedTime / _timeToWaypoint;
        elapsedPecentage = Mathf.SmoothStep(0f, 1f, elapsedPecentage);  // Smooth at the limit of the elapsedPercentage
        //Debug.Log("Elapsed Percentage: " + elapsedPecentage.ToString("P"));

        transform.position = Vector3.Lerp(_previousWaypoint.position, _targetWaypoint.position, elapsedPecentage);      // Movement
        transform.rotation = Quaternion.Lerp(_previousWaypoint.rotation, _targetWaypoint.rotation, elapsedPecentage);   // Rotation

        if (elapsedPecentage >= 1)
        {
            TargetNextWaypoint();
        }
    }

    private void TargetNextWaypoint()
    {
        // Setting waypoint index and getting the transform pf the previous and target waypoint

        //Debug.Log("Previous Waypoint Index: " + _waypointIndex);

        _previousWaypoint = _waypointPath.GetWaypoint(_waypointIndex);
        //Debug.Log("Previous Waypoint (transform): " + _previousWaypoint);

        _waypointIndex = _waypointPath.GetNextWaypointIndex(_waypointIndex);
        //Debug.Log("Target Waypoint Index: " + _waypointIndex);

        _targetWaypoint = _waypointPath.GetWaypoint(_waypointIndex);
        //Debug.Log("Target Waypoint (transform): " + _targetWaypoint);

        _elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(_previousWaypoint.position, _targetWaypoint.position);
        _timeToWaypoint = distanceToWaypoint / _speed;
    }

    //private void OnTriggerEnter(Collider other) // Platform must have a scale of (1,1,1) so that the child don't break upon exit
    //{
    //    other.transform.SetParent(transform);
    //    _objectOnPlatform = other;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    other.transform.SetParent(null);
    //    _objectOnPlatform = other;
    //}
}
