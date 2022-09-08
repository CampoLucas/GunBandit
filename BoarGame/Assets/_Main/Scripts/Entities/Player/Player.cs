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
    public Inventory WeaponInventory { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<PlayerInputHandler>();
        _rotation = GetComponent<IRotation>();
        WeaponInventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        _input.OnFireInput += Fire;
        _input.OnThrowInput += Throw;
        _input.OnReloadInput += Reload;
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
        _input.OnReloadInput -= Reload;
    }
    
    private void Rotate(Vector2 mousePos) => _rotation.Rotate(mousePos);

    private void Fire()
    {
        var gun = WeaponInventory.CurrentWeapon != null ? WeaponInventory.CurrentWeapon as Gun : null;
        if (gun == null) return;
        gun.Fire();
    }
    private void Reload()
    {
        var gun = WeaponInventory.CurrentWeapon != null ? WeaponInventory.CurrentWeapon as Gun : null;
        if (gun == null) return;
        gun.Reload();
    }

    private void Throw()
    {
        if (WeaponInventory.CurrentWeapon == null) return;
        WeaponInventory.CurrentWeapon.Throw();
        //WeaponInventory.CurrentWeapon = null;
        //Remove the weapon from inventory
    }
}
