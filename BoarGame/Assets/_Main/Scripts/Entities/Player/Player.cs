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
    private IMovement _movement;

    public Inventory Inventory;


    private void Awake()
    {
        _input = GetComponent<PlayerInputHandler>();
        _rotation = GetComponent<IRotation>();
        _movement = GetComponent<IMovement>();
        Inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        _input.OnFire += Fire;
        _input.OnThrow += Throw;
        _input.OnReload += Reload;
    }

    private void Update()
    {
        Move(_input.MovementInput);
        Rotate(_input.MousePosition);
    }

    private void OnDisable()
    {
        _input.OnFire -= Fire;
        _input.OnThrow -= Throw;
        _input.OnReload -= Reload;
    }
    private void Move(Vector2 direction) => _movement.Move(direction);
    private void Rotate(Vector2 mousePos) => _rotation.Rotate(mousePos);

    private void Fire()
    {
        if(!Inventory.CurrentWeapon()) return;
        Inventory.CurrentWeapon().Attack();
        var gun = Inventory.CurrentWeapon() as Ranged;
        if (!gun) return;
    }
    private void Reload()
    {
        var gun = Inventory.CurrentWeapon() as Ranged;
        if (gun == null) return;
        gun.Reload();
    }

    private void Throw()
    {
        if(Inventory.CurrentWeapon() == null) return;
        Inventory.CurrentWeapon().Throw();
        Inventory.DropItem();
        //WeaponInventory.CurrentWeapon = null;
        //Remove the weapon from inventory
    }
}
