using System;
using System.Collections;
using UnityEngine;

public class HammerScript : MonoBehaviour
{
    private float _pausedGameHammerCollisionTimeLeft;
    private UserInterFaceManager _userInterFaceManager;


    [SerializeField] private AudioClip hammerCollisionSoundEffect;
    [SerializeField] private GameObject barrelDestroyedEffect;
    private GameObject _barrelDestroyedEffectInstance;


    private void Awake()
    {
        _userInterFaceManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterFaceManager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PauseManager.Instance.PauseGameForTime(hammerCollisionSoundEffect.length);
        AudioManagerScript.Instance.PlaySoundFxClip(hammerCollisionSoundEffect, transform, .02f,
            "SmashGameObjectSoundEffect");
        _barrelDestroyedEffectInstance =
            Instantiate(barrelDestroyedEffect, other.transform.position, Quaternion.identity);


        if (other.collider.CompareTag("Barrel"))
        {
            _userInterFaceManager.ChangeMarioScoreNoSound(300, transform);
            Destroy(other.gameObject);
        }

        if (other.collider.CompareTag("Fireball"))
        {
            _userInterFaceManager.ChangeMarioScoreNoSound(300, transform);
            Destroy(other.gameObject);
        }

        if (other.collider.CompareTag("CementTub"))
        {
            _userInterFaceManager.ChangeMarioScoreNoSound(300, transform);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (!PauseManager.Instance.IsPaused)
        {
            Destroy(_barrelDestroyedEffectInstance);
            Destroy(GameObject.FindGameObjectWithTag("SmashGameObjectSoundEffect"));
            Destroy(GameObject.FindGameObjectWithTag("ScoreEffectText"));
        }
    }
}