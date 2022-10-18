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
    private PickUpWeapon _pick;

    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        _input = GetComponent<PlayerInputHandler>();
        _rotation = GetComponent<IRotation>();
        _movement = GetComponent<IMovement>();
        _pick = GetComponent<PickUpWeapon>();
        Inventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        _input.OnFire += Fire;
        _input.OnThrow += Throw;
        _input.OnReload += Reload;
        _input.OnScrollUp += SwapWeaponUp;
        _input.OnScrollDown += SwapWeaponDown;
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
        _input.OnScrollUp -= SwapWeaponUp;
        _input.OnScrollDown -= SwapWeaponDown;
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
        if(!Inventory.HasAWeapon()) return;
        //Hacer que el jugador lanze el arma
        Inventory.CurrentWeapon().Throw();
        Drop();
        
        //WeaponInventory.CurrentWeapon = null;
        //Remove the weapon from inventory
    }

    private void PickUpWeapon()
    {
        if(!Inventory.HasAWeapon()) return;
        _pick.PickUp(Inventory.CurrentWeapon());
        var stats = Inventory.CurrentWeapon().GetData() as WeaponSO;
        ChangeAttackInput(stats.Hold);
    }

    private void Drop()
    {
        if(!Inventory.HasAWeapon()) return;
        _pick.Drop();
        Inventory.DropItem();
        if(!Inventory.HasAWeapon()) return;
        var stats = Inventory.CurrentWeapon().GetData() as WeaponSO;
        ChangeAttackInput(stats.Hold);
    }

    private void SwapWeaponUp()
    {
        if(!Inventory.HasAWeapon()) return;
        Inventory.ChangeWeapon(true);
        var stats = Inventory.CurrentWeapon().GetData() as WeaponSO;
        ChangeAttackInput(stats.Hold);
    }
    
    private void SwapWeaponDown()
    {
        if(!Inventory.HasAWeapon()) return;
        Inventory.ChangeWeapon(false);
        var stats = Inventory.CurrentWeapon().GetData() as WeaponSO;
        ChangeAttackInput(stats.Hold);
    }

    public void ChangeAttackInput(bool canHold)
    {
        _input.SetHolding(canHold);
    }
}
