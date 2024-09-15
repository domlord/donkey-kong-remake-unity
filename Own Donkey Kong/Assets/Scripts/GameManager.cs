using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int NumberOfLives { get; private set; }
    public int MarioScore { get; private set; }
    public static GameManager Instance { get; private set; }
    [SerializeField] private AudioSource levelOneBackgroundMusic;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        NumberOfLives = 3;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
    }

    public void ChangeNumberOfLives(int valueToChange)
    {
        NumberOfLives += valueToChange;
    }

    public void ChangeScore(int valueToChangeScoreBy)
    {
        MarioScore += valueToChangeScoreBy;
    }
}