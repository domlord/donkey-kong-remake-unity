using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PaulineScript : MonoBehaviour
{
    [SerializeField] private Animator paulineAnimator;

    // [SerializeField] private float timeToPlayHelpAnimationFor;
    [SerializeField] private float timeToPlayIdleAnimationFor;
    // private float timer;
    private int _paulineHelpAnimatorIndex;
    private int _paulineIdleAnimatorIndex;
    private int _paulineIsAnimatedIndex;
    private void Awake()
    {
        _paulineIdleAnimatorIndex = Animator.StringToHash("isIdlePauline");
        _paulineHelpAnimatorIndex = Animator.StringToHash("isHelp");
        _paulineIsAnimatedIndex = Animator.StringToHash("paulineIsAnimated");
        paulineAnimator.SetBool(_paulineHelpAnimatorIndex, true);
        paulineAnimator.SetBool(_paulineIdleAnimatorIndex, false);
        paulineAnimator.SetBool(_paulineIsAnimatedIndex, false);
    }

    // private void PlayIdleAnimation(float timerToPlayIdleAnimationFor)
    // {
    //     timer += Time.deltaTime;
    //     if (timer >= timerToPlayIdleAnimationFor)
    //     {
    //         paulineAnimator.SetBool(_paulineIdleAnimatorIndex, false);
    //         paulineAnimator.SetBool(_paulineHelpAnimatorIndex, true);
    //     }
    // }
    //
    // private void PlayHelpAnimation(float timeToPlayHelpAnimationFor)
    // {
    //     timer += Time.deltaTime;
    //     if (timer >= timeToPlayHelpAnimationFor)
    //     {
    //         paulineAnimator.SetBool(_paulineIdleAnimatorIndex, true);
    //         paulineAnimator.SetBool(_paulineHelpAnimatorIndex, false);
    //     }
    // }

    private void Update()
    {
        // StartCoroutine(PlayIdleForNSeconds(timeToPlayHelpAnimationFor, timeToPlayIdleAnimationFor));
    }

    // private IEnumerator PlayIdleForNSeconds(float timeToPlayHelpFor, float timeToBeIdleFor)
    // {
    //     paulineAnimator.SetBool(_paulineHelpAnimatorIndex, true);
    //     paulineAnimator.SetBool(_paulineIdleAnimatorIndex, false);
    //     yield return new WaitForSeconds(timeToPlayHelpFor);
    //     paulineAnimator.SetBool(_paulineIdleAnimatorIndex, true);
    //     paulineAnimator.SetBool(_paulineHelpAnimatorIndex, false);
    //     yield return new WaitForSeconds(timeToBeIdleFor);
    // }
}