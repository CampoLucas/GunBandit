using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls _inputActions;

    private Vector2 _movementInput;
    private Vector2 _mousePos;
    private Action _onFireInput;
    private Action _onThrowInput;
    private bool _fireInput;
    
    /// <summary>
    /// Gets horizontal and vertical input
    /// </summary>
    public Vector2 MovementInput => _movementInput;
    public Vector2 MousePosition => _mousePos;
    public Action OnFireInput
    {
        get => _onFireInput;
        set => _onFireInput = value;
    }
    public Action OnThrowInput
    {
        get => _onThrowInput;
        set => _onThrowInput = value;
    }

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerControls();
            _inputActions.Player.Movement.performed += inputActions => _movementInput = inputActions.ReadValue<Vector2>();
            _inputActions.Player.Point.performed += i => _mousePos = i.ReadValue<Vector2>();
            _inputActions.Player.Fire.started += i => _fireInput = true;
            _inputActions.Player.Fire.canceled += i => _fireInput = false;
            _inputActions.Player.Throw.performed += i => _onThrowInput?.Invoke();
        }
        _inputActions.Enable();
    }

    private void Update()
    {
        if(_fireInput)
            _onFireInput?.Invoke();
    }

    private void OnDisable() => _inputActions.Disable();

    
}
