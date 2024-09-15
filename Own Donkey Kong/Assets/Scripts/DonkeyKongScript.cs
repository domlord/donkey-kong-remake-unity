using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DonkeyKongScript : MonoBehaviour
{
    [SerializeField] private Animator donkeyKongAnimator;
    private BarrelSpawner _barrelSpawner;
    private int _donkeyKongBarrelSpawnAnimationIndex;
    private int _donkeyKongIdleAnimationIndex;
    private int _donkeyKongHitChestAnimationIndex;

    private void Awake()
    {
        _donkeyKongBarrelSpawnAnimationIndex = Animator.StringToHash("isSpawningBarrels");
        _donkeyKongIdleAnimationIndex = Animator.StringToHash("isIdle");
        _donkeyKongHitChestAnimationIndex = Animator.StringToHash("isHittingChest");
        _barrelSpawner = GameObject.FindGameObjectWithTag("BarrelSpawner").GetComponent<BarrelSpawner>();
    }

    private void Start()
    {
        donkeyKongAnimator.SetBool(_donkeyKongBarrelSpawnAnimationIndex, true);
    }
}