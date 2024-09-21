using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance;
    private AudioSource[] _audioSources;

    public bool IsPaused { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PauseGame()
    {
        IsPaused = true;
        Time.timeScale = 0f;
        InputManager.PlayerInput.DeactivateInput();
        _audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audioSource in _audioSources)
        {
            audioSource.Stop();
        }
    }

    public void ResumeGame()
    {
        IsPaused = false;
        Time.timeScale = 1f;
        InputManager.PlayerInput.ActivateInput(); // need to patch to switch to ui elements if needed
        _audioSources = FindObjectsOfType<AudioSource>();
        foreach (var audioSource in _audioSources)
        {
            if (audioSource.isActiveAndEnabled)
            {
                audioSource.Play();
            }
        }
    }

    public void PauseGameForTime(float timeToPauseGame)
    {
        StartCoroutine(PauseGameEnumerator(timeToPauseGame));
    }

    private IEnumerator PauseGameEnumerator(float timeToPauseGame)
    {
        PauseGame();
        var gamePausedTimer = Time.realtimeSinceStartup + timeToPauseGame;
        while (Time.realtimeSinceStartup < gamePausedTimer)
        {
            yield return 0;
        }

        ResumeGame();
    }
}