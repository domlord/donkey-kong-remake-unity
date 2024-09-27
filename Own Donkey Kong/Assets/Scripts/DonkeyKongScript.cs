using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DonkeyKongScript : MonoBehaviour
{
    [SerializeField] private Animator donkeyKongAnimator;
    private int _donkeyKongBarrelSpawnAnimationIndex;

    private void Awake()
    {
        _donkeyKongBarrelSpawnAnimationIndex = Animator.StringToHash("isSpawningBarrels");
    }

    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "Level 1")
        {
            donkeyKongAnimator.SetBool(_donkeyKongBarrelSpawnAnimationIndex, true);
        }

        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            donkeyKongAnimator.Play("DonkeyKongHitChestLevel2");
            donkeyKongAnimator.SetBool("isLevel2", true);
        }
    }
}