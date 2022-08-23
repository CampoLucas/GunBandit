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

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerControls();
            _inputActions.Player.Movement.performed += inputActions => _movementInput = inputActions.ReadValue<Vector2>();
            _inputActions.Player.Point.performed += i => _mousePos = i.ReadValue<Vector2>();
            _inputActions.Player.Fire.performed += i => _onFireInput?.Invoke();
        }
        _inputActions.Enable();
    }

    private void OnDisable() => _inputActions.Disable();

    
}
