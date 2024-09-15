using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OilBarrelScript : MonoBehaviour
{
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private Animator oilBarrelAnimator;
    [SerializeField] private float fireballSpawnRate;
    [SerializeField] private float fireballDelay50M;

    private float _timer;
    private int _hasFireBallJumpedOutAnimatorIndex;

    private void Awake()
    {
        _hasFireBallJumpedOutAnimatorIndex = Animator.StringToHash("hasFireBallJumpedOut");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("FirstBarrel"))
        {
            foreach (GameObject fireball in fireballs)
            {
                StartCoroutine(DelayBeforeEnablingFireball(fireballSpawnRate));
                Debug.Log("delayed");
                fireball.gameObject.SetActive(true);
            }
        }
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > fireballDelay50M && SceneManager.GetActiveScene().name == "Level 2")
        {
            foreach (GameObject fireball in fireballs)
            {
                StartCoroutine(DelayBeforeEnablingFireball(fireballSpawnRate));
                fireball.gameObject.SetActive(true);
            }
        }

        if (SceneManager.GetActiveScene().name == "Level 1" && fireballs[0].activeInHierarchy)
        {
            oilBarrelAnimator.SetBool(_hasFireBallJumpedOutAnimatorIndex, true);
        }
    }

    private static IEnumerator DelayBeforeEnablingFireball(float fireBallSpawnRate)
    {
        yield return new WaitForSeconds(fireBallSpawnRate);
    }
}