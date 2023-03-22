using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It handles all the player actions
/// Common ground for all player scripts
/// </summary>
public class Player : Character
{
    private PlayerInputHandler _input; //player
    

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<PlayerInputHandler>();
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

    private void Reload()
    {
        var gun = Inventory.CurrentWeapon() as Ranged;
        if (gun == null) return;
        gun.Reload();
    }

    private void Throw()
    {
        if(!Inventory.HasCurrentWeapon()) return;
        //Hacer que el jugador lanze el arma
        Inventory.CurrentWeapon().Throw();
        Drop();
        
        //WeaponInventory.CurrentWeapon = null;
        //Remove the weapon from inventory
    }

    protected override void PickUpWeapon() //p
    {
        base.PickUpWeapon();
        ChangeAttackInput();
    }

    protected override void Drop()
    {
        base.Drop();
        ChangeAttackInput();
    }

    protected override void SwapWeaponUp()
    {
        base.SwapWeaponUp();
        ChangeAttackInput();
    }
    
    protected override void SwapWeaponDown()
    {
        base.SwapWeaponDown();
        ChangeAttackInput();
    }

    private void ChangeAttackInput()
    {
        if(!Inventory.HasCurrentWeapon()) return;
        var stats = Inventory.CurrentWeapon().GetData() as WeaponSO;
        _input.SetHolding(stats && stats.Hold);
    }
}
