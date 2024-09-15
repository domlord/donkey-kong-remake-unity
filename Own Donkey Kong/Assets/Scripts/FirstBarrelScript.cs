using System;
using System.Collections;
using UnityEngine;

public class FirstBarrelScript : MonoBehaviour
{
    [SerializeField] private Rigidbody2D firstBarrelRigidBody2d;
    [SerializeField] private float firstBarrelDescendSpeed;
    [SerializeField] private float timeToPauseOnPlatformCollision;
    [SerializeField] private float moveBarrelToOilBarrelSpeed;
    [SerializeField] private Animator firstBarrelAnimator;
    private int _firstBarrelMoveLeftAnimatorIndex;

    private float _barrelMoveHorizontalSpeed;

    private void Awake()
    {
        _firstBarrelMoveLeftAnimatorIndex = Animator.StringToHash("firstBarrelMoveLeftAnimator");
    }

    private void Update()
    {
        firstBarrelRigidBody2d.velocity = new Vector2(_barrelMoveHorizontalSpeed, -firstBarrelDescendSpeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FirstBarrelCollisionTrigger"))
        {
            StartCoroutine(PauseForNSecondsChangeObjectLayer(timeToPauseOnPlatformCollision, gameObject));
        }


        if (other.CompareTag("FirstBarrelColisionTrueTrigger"))
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }

        if (other.CompareTag("MoveFirstBarrelToOilBarrel"))
        {
            _barrelMoveHorizontalSpeed = -moveBarrelToOilBarrelSpeed;
            firstBarrelDescendSpeed = 0;
        }

        if (other.CompareTag("OilBarrel"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("FirstBarrelMoveLeftAnimatorTrigger"))
        {
            Debug.Log("trig");
            firstBarrelAnimator.SetBool(_firstBarrelMoveLeftAnimatorIndex, true);
        }
    }

    private static IEnumerator PauseForNSecondsChangeObjectLayer(float pauseCollisionTimeSeconds,
        GameObject objectToChangeLayer)
    {
        yield return new WaitForSeconds(pauseCollisionTimeSeconds);
        objectToChangeLayer.layer = LayerMask.NameToLayer("Passable Objects");
    }
}