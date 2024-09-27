using System;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    [SerializeField] private Transform[] pathToMoveObject;
    private int _currentPath;
    [SerializeField] private float fireballMoveSpeed;
    private int fireballHammeringAnimatorIndex;
    [SerializeField] private Animator fireballAnimator;


    private void OnEnable()
    {
        MarioController.OnMarioHammering += ChangeFireballToBlue;
        MarioController.OnMarioNotHammering += ChangeFireballToRed;
    }

    private void OnDisable()
    {
        MarioController.OnMarioHammering -= ChangeFireballToBlue;
        MarioController.OnMarioNotHammering -= ChangeFireballToRed;
    }

    private void Start()
    {
        transform.position = pathToMoveObject[_currentPath].transform.position;
    }

    private void Awake()
    {
        fireballHammeringAnimatorIndex = Animator.StringToHash("isMarioHammering");
    }

    private void Update()
    {
        if (_currentPath <= pathToMoveObject.Length - 1)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                pathToMoveObject[_currentPath].transform.position,
                fireballMoveSpeed * Time.deltaTime);

            if (transform.position == pathToMoveObject[_currentPath].transform.position)

            {
                _currentPath++;
            }
        }

        if (_currentPath == pathToMoveObject.Length)
        {
            _currentPath = 0;
        }
    }

    private void ChangeFireballToBlue()
    {
        Debug.Log("mario hammering");
        fireballAnimator.SetBool(fireballHammeringAnimatorIndex, true);
    }

    private void ChangeFireballToRed()
    {
        Debug.Log("mario not hammering");
        fireballAnimator.SetBool(fireballHammeringAnimatorIndex, false);
    }
}