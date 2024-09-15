using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    [SerializeField] private GameObject[] pathToMoveElevator;
    [SerializeField] private GameObject elevator;
    [SerializeField] private float elevatorMoveSpeed;
    private int _elevatorPathIndex;
    private GameObject _localElevator;


    private void Start()
    {
        _localElevator = Instantiate(elevator, pathToMoveElevator[0].transform.position, transform.rotation);
    }

    private void Update()
    {
        if (_elevatorPathIndex <= pathToMoveElevator.Length - 1)
        {
            _localElevator.transform.position = Vector2.MoveTowards(_localElevator.transform.position,
                pathToMoveElevator[_elevatorPathIndex].transform.position,
                elevatorMoveSpeed * Time.deltaTime);

            if (_localElevator.transform.position == pathToMoveElevator[_elevatorPathIndex].transform.position)

            {
                _elevatorPathIndex++;
            }
        }

        if (_elevatorPathIndex == pathToMoveElevator.Length)
        {
            Destroy(_localElevator.gameObject);
            _elevatorPathIndex = 0;
            _localElevator = Instantiate(elevator, pathToMoveElevator[0].transform.position, transform.rotation);
        }
    }
}