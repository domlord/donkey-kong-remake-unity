using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public static PlayerInput PlayerInput;
    public Vector2 MoveInput { get; private set; }
    public bool ClimbJustPressed { get; private set; }
    public bool ClimbIsPressed { get; private set; }
    public bool ClimbWasReleased { get; private set; }
    public bool JumpJustPressed { get; private set; }
    public bool JumpIsPressed { get; private set; }
    public bool JumpWasReleased { get; private set; }
    public bool MoveIsPressed { get; private set; }
    public bool GoDownLadderJustPressed { get; private set; }
    public bool GoDownLadderIsPressed { get; private set; }
    public bool GoDownLadderJustReleased { get; private set; }


    private InputAction _climbInputAction;
    private InputAction _moveInputAction;
    private InputAction _jumpInputAction;
    private InputAction _goDownLadderInputAction;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        PlayerInput = GetComponent<PlayerInput>();

        _climbInputAction = PlayerInput.actions["Climb"];
        _moveInputAction = PlayerInput.actions["Move"];
        _jumpInputAction = PlayerInput.actions["Jump"];
        _goDownLadderInputAction = PlayerInput.actions["GoDownLadder"];

        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        MoveInput = _moveInputAction.ReadValue<Vector2>();
        MoveIsPressed = _moveInputAction.IsPressed();
        ClimbJustPressed = _climbInputAction.WasPressedThisFrame();
        ClimbWasReleased = _climbInputAction.WasReleasedThisFrame();
        ClimbIsPressed = _climbInputAction.IsPressed();
        JumpJustPressed = _jumpInputAction.WasPressedThisFrame();
        JumpWasReleased = _jumpInputAction.WasReleasedThisFrame();
        JumpIsPressed = _jumpInputAction.IsPressed();
        GoDownLadderJustPressed = _goDownLadderInputAction.WasPressedThisFrame();
        GoDownLadderJustReleased = _goDownLadderInputAction.WasReleasedThisFrame();
        GoDownLadderIsPressed = _goDownLadderInputAction.IsPressed();
    }
}