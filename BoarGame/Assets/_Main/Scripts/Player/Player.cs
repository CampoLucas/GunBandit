using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// It handles all the player actions
/// Common ground for all player scripts
/// </summary>
public class Player : MonoBehaviour
{
    private PlayerInputHandler _input;
    private IMovement _movement;
    private IRotation _rotation;
    private Weapon _weapon;

    private void Awake()
    {
        _input = GetComponent<PlayerInputHandler>();
        _movement = GetComponent<IMovement>();
        _rotation = GetComponent<IRotation>();
        _weapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        _input.OnFireInput += Fire;
    }

    private void Update()
    {
        Move(_input.MovementInput);
        Rotate(_input.MousePosition);
    }

    private void OnDisable()
    {
        _input.OnFireInput -= Fire;
    }

    private void Move(Vector2 direction) => _movement.Move(direction);
    private void Rotate(Vector2 mousePos) => _rotation.Rotate(mousePos);

    private void Fire()
    {
        var gun = _weapon as Gun;
        if (gun != null) gun.Fire();
    }
}
