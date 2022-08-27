using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// It handles all the player actions
/// Common ground for all player scripts
/// </summary>
public class Player : Entity
{
    private PlayerInputHandler _input;
    private IRotation _rotation;
    private Weapon _weapon;

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<PlayerInputHandler>();
        _rotation = GetComponent<IRotation>();
        _weapon = GetComponentInChildren<Weapon>();
        Debug.Log(_weapon.Data.Id);
    }

    private void Start()
    {
        _input.OnFireInput += Fire;
        _input.OnThrowInput += Throw;
    }

    private void Update()
    {
        Move(_input.MovementInput);
        Rotate(_input.MousePosition);
    }

    private void OnDisable()
    {
        _input.OnFireInput -= Fire;
        _input.OnThrowInput -= Throw;
    }
    
    private void Rotate(Vector2 mousePos) => _rotation.Rotate(mousePos);

    private void Fire()
    {
        var gun = _weapon != null ? _weapon as Gun : null;
        if (gun == null) return;
        gun.Fire();
    }

    private void Throw()
    {
        if (_weapon == null) return;
        _weapon.Throw();
        _weapon = null;
    }
}
