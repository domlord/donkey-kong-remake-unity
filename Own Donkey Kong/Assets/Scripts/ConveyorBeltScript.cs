using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBeltScript : MonoBehaviour
{
    private List<GameObject> _objectsOnConveyorBelt;
    [SerializeField] private float movementForce;
    [SerializeField] private bool startConveyorDirectionRight;
    [SerializeField] private bool startConveyorDirectionLeft;
    [SerializeField] private Animator conveyorBeltLeftWheelAnimator;
    [SerializeField] private Animator conveyorBeltRightWheelAnimator;
    private bool _isMovingLeft;
    private int _isConveyorMovingLeftAnimatorIndex;
    private int _isConveyorMovingRightAnimatorIndex;

    private bool _isMovingRight;

    // private bool _hasTouchedBelt;
    private float _beltMovementTimer;
    [SerializeField] private float howLongBeltIsMovingDirection;

    private void Awake()
    {
        _isConveyorMovingLeftAnimatorIndex = Animator.StringToHash("isConveyorBeltMovingLeft");
        _isConveyorMovingRightAnimatorIndex = Animator.StringToHash("isConveyorBeltMovingRight");
    }

    private void Start()
    {
        if (startConveyorDirectionRight)
        {
            _isMovingRight = true;
        }
        else if (startConveyorDirectionLeft)
        {
            _isMovingLeft = true;
        }

        _beltMovementTimer = howLongBeltIsMovingDirection;
    }

    private void Update()
    {
        if (_isMovingLeft)
        {
            conveyorBeltLeftWheelAnimator.SetBool(_isConveyorMovingLeftAnimatorIndex, true);
            conveyorBeltLeftWheelAnimator.SetBool(_isConveyorMovingRightAnimatorIndex, false);
            conveyorBeltRightWheelAnimator.SetBool(_isConveyorMovingLeftAnimatorIndex, true);
            conveyorBeltRightWheelAnimator.SetBool(_isConveyorMovingRightAnimatorIndex, false);
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

        else if (_isMovingRight)
        {
            conveyorBeltLeftWheelAnimator.SetBool(_isConveyorMovingLeftAnimatorIndex, false);
            conveyorBeltLeftWheelAnimator.SetBool(_isConveyorMovingRightAnimatorIndex, true);
            conveyorBeltRightWheelAnimator.SetBool(_isConveyorMovingLeftAnimatorIndex, false);
            conveyorBeltRightWheelAnimator.SetBool(_isConveyorMovingRightAnimatorIndex, true);
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