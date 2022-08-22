using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerControls _inputActions;

    private Vector2 _movementInput;
    [SerializeField] private Vector2 _mousePos;
    /// <summary>
    /// Gets horizontal and vertical input
    /// </summary>
    public Vector2 MovementInput => _movementInput;
    public Vector2 MousePosition => _mousePos;

    private void OnEnable()
    {
        if (_inputActions == null)
        {
            _inputActions = new PlayerControls();
            _inputActions.Player.Movement.performed += inputActions => _movementInput = inputActions.ReadValue<Vector2>();
            _inputActions.Player.Point.performed += i => _mousePos = i.ReadValue<Vector2>();
        }
        _inputActions.Enable();
    }

    private void OnDisable() => _inputActions.Disable();

    
}
