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
    private IRotation _rotation;
    private IMovement _movement;

    public PlayerInputHandler Input { get; private set; }
    public PickUpWeapon Pick { get; private set; }
    public Inventory Inventory { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Input = GetComponent<PlayerInputHandler>();
        _rotation = GetComponent<IRotation>();
        _movement = GetComponent<IMovement>();
        Pick = GetComponent<PickUpWeapon>();
        Inventory = GetComponent<Inventory>();
    }

    protected override void Start()
    {
        base.Start();
        Input.OnFire += Fire;
        Input.OnThrow += Throw;
        Input.OnReload += Reload;
        Input.OnScrollUp += SwapWeaponUp;
        Input.OnScrollDown += SwapWeaponDown;
    }

    private void Update()
    {
        Move(Input.MovementInput);
        Rotate(Input.MousePosition);
    }

    private void OnDisable()
    {
        Input.OnFire -= Fire;
        Input.OnThrow -= Throw;
        Input.OnReload -= Reload;
        Input.OnScrollUp -= SwapWeaponUp;
        Input.OnScrollDown -= SwapWeaponDown;
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
        Inventory.CurrentWeapon().Throw();
        Drop();
    }

    public void PickUpWeapon()
    {
        if(!Inventory.HasAWeapon()) return;
        Pick.PickUp(Inventory.CurrentWeapon());
    }

    private void Drop()
    {
        if(!Inventory.HasAWeapon()) return;
        Pick.Drop();
        Inventory.DropItem();
    }

    private void SwapWeaponUp()
    {
        if(!Inventory.HasAWeapon()) return;
        Inventory.ChangeWeapon(true);
    }
    
    private void SwapWeaponDown()
    {
        if(!Inventory.HasAWeapon()) return;
        Inventory.ChangeWeapon(false);
    }
}
