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
    [SerializeField] private BoxCollider2D topOfLadderBoxCollider;
    [SerializeField] private BoxCollider2D middleOfLadderBoxCollider;
    private float _ladderAtTopTimer;
    private float _ladderAtBottomTimer;
    private bool _shouldBeMoving;
    private Transform _targetPosition;


    private void Start()
    {
        topOfLadderBoxCollider.enabled = false;
        middleOfLadderBoxCollider.enabled =
            false; // may be better to just disable those components which are not required instead of whole game object
        _targetPosition = downPoint;
        _shouldBeMoving = true;
    }

    private void Update()
    {
        if (_shouldBeMoving)
            MoveTowards(gameObject, _targetPosition, moveSpeed);

        // if object has been at a waypoint for a certain period of time, set bool to true
        if (Vector2.Distance(transform.position, upPath.position) < Double.Epsilon)
        {
            topOfLadderBoxCollider.enabled = true; //when the ladder connects with the platform, enable top
            middleOfLadderBoxCollider.enabled = true;
            StartCoroutine(SetNextPositionAfterNSeconds(timeToStayConnected, downPoint));
            _shouldBeMoving = false;
            // set timer when at waypoint
        }

        // if object has been at a waypoint for a certain period of time, set bool to true
        // when object reaches a waypoint. begin timer. when timer has finished, set a bool value to allow the object to begin movement to the other waypoint
        else if (Vector2.Distance(transform.position, downPoint.position) < Double.Epsilon)
        {
            topOfLadderBoxCollider.enabled = false;
            middleOfLadderBoxCollider.enabled = false;
            StartCoroutine(SetNextPositionAfterNSeconds(timeToStayDisconnected, upPath));
            _shouldBeMoving = false;
        }
    }

    private IEnumerator SetNextPositionAfterNSeconds(float waitTime, Transform targetPosition)
    {
        yield return new WaitForSeconds(waitTime);
        _targetPosition = targetPosition;
        _shouldBeMoving = true;
    }

    private void MoveTowards(GameObject objectToMove, Transform positionToMoveTo, float speedToMoveTowards)
    {
        topOfLadderBoxCollider.enabled = false;
        objectToMove.transform.position = Vector2.MoveTowards(objectToMove.transform.position,
            positionToMoveTo.position, speedToMoveTowards * Time.deltaTime);
    }
}