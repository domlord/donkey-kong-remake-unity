using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaulineItemsScript : MonoBehaviour
{
    private UserInterFaceManager _userInterFaceManager;

    private void Awake()
    {
        _userInterFaceManager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<UserInterFaceManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Mario"))
        {
            if (SceneManager.GetActiveScene().name == "Level 1")
            {
                _userInterFaceManager.ChangeMarioScore(300, transform);
            }

            else if (SceneManager.GetActiveScene().name == "Level 2")
            {
                _userInterFaceManager.ChangeMarioScore(500, transform);
            }
            else
            {
                _userInterFaceManager.ChangeMarioScore(800, transform);
            }

            Destroy(gameObject);
        }
    }
}