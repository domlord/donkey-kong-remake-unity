using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltScript : MonoBehaviour
{
    private List<GameObject> _objectsOnConveyorBelt;
    [SerializeField] private float movementForce;
    private bool _isMovingLeft;
    private bool _isMovingRight;
    private bool _hasTouchedBelt;
    private float _beltMovementTimer;
    [SerializeField] private float howLongBeltIsMovingDirection;

    private void Awake()
    {
        _isMovingLeft = true;
    }

    private void Update()
    {
        if (_hasTouchedBelt)
        {
            if (_isMovingLeft)
            {
                if (_beltMovementTimer > 0)
                {
                    _beltMovementTimer -= Time.deltaTime;
                }
                else
                {
                    _isMovingLeft = false;
                    _isMovingRight = true;
                    _beltMovementTimer = howLongBeltIsMovingDirection;
                }
            }

            if (_isMovingRight)
            {
                if (_beltMovementTimer > 0)
                {
                    _beltMovementTimer -= Time.deltaTime;
                }
                else
                {
                    _isMovingRight = false;
                    _isMovingLeft = true;
                    _beltMovementTimer = howLongBeltIsMovingDirection;
                }
            }
        }
    }

    private void OnCollisionEnter2D()
    {
        _hasTouchedBelt = true;
        _beltMovementTimer = howLongBeltIsMovingDirection;
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Mario"))
        {
            if (_isMovingLeft)
            {
                other.gameObject.GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2(-movementForce * movementForce * 10, 0));
            }

            if (_isMovingRight)
            {
                other.gameObject.GetComponent<Rigidbody2D>()
                    .AddForce(new Vector2(movementForce * movementForce * 10, 0));
            }
        }
        else
        {
            if (_isMovingLeft)
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-movementForce, 0);
            }

            if (_isMovingRight)
            {
                other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(movementForce, 0);
            }
        }
    }
}