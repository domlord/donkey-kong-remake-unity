using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLadderScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    [SerializeField]
    private Transform upPath; //An array of transforms (just the position, scale and rotation of any given object)

    // [SerializeField] private Transform[] path;
    [SerializeField] private Transform downPoint;

    private int _pathIndex; //Did you know that the default value of an int is 0?? crazy 
    private Transform _targetWayPoint;
    [SerializeField] private float timeToStayConnected;
    [SerializeField] private float timeToStayDisconnected;
    private float _ladderAtTopTimer;
    private float _ladderAtBottomTimer;
    private bool _hasReachedBottom;
    private bool _hasReachedTop;
    private Transform _targetPosition;


    private void Start()
    {
        _targetPosition = downPoint;
    }

    private void Update()
    {
        MoveTowards(gameObject, _targetPosition, moveSpeed);

        // if object has been at a waypoint for a certain period of time, set bool to true
        if (Vector2.Distance(transform.position, upPath.position) < Double.Epsilon)
        {
            StartCoroutine(SetNextPositionAfterNSeconds(timeToStayConnected, downPoint));
            // set timer when at waypoint
        }

        // if object has been at a waypoint for a certain period of time, set bool to true
        // when object reaches a waypoint. begin timer. when timer has finished, set a bool value to allow the object to begin movement to the other waypoint
        if (Vector2.Distance(transform.position, downPoint.position) < Double.Epsilon)
        {
            StartCoroutine(SetNextPositionAfterNSeconds(timeToStayDisconnected, upPath));
        }
    }

    private IEnumerator SetNextPositionAfterNSeconds(float waitTime, Transform targetPosition)
    {
        yield return new WaitForSeconds(waitTime);
        _targetPosition = targetPosition;
    }

    private void MoveTowards(GameObject objectToMove, Transform positionToMoveTo, float speedToMoveTowards)
    {
        objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position,
            positionToMoveTo.position, speedToMoveTowards * Time.deltaTime);
    }
}