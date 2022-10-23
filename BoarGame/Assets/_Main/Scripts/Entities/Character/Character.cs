using UnityEngine;
using System;

public class Character : Entity
{
    protected IRotation Rotation;
    protected IMovement Movement;
    protected IDamageable Damageable;
    protected PickUpWeapon Pick;

    public IInventory Inventory { get; private set; }
    
    protected virtual void Awake()
    {
        Rotation = GetComponent<IRotation>();
        Movement = GetComponent<IMovement>();
        Damageable = GetComponent<IDamageable>();
        Inventory = GetComponent<IInventory>();
        Pick = GetComponent<PickUpWeapon>();
    }

    protected virtual void Move(Vector2 direction) => Movement.Move(direction);
    protected virtual void Rotate(Vector2 position) => Rotation.Rotate(position);

    protected virtual void Fire()
    {
        if (!Inventory.HasCurrentWeapon()) return;
        Inventory.CurrentWeapon().Attack();
    }

    protected virtual void PickUpWeapon()
    {
        if (!Inventory.HasCurrentWeapon()) return;
        Pick.PickUp(Inventory.CurrentWeapon());
    }

    protected virtual void Drop()
    {
        if(!Inventory.HasCurrentWeapon()) return;
        Pick.Drop();
        Inventory.DropItem();
    }
    
    protected virtual void SwapWeaponUp()
    {
        if(!Inventory.HasCurrentWeapon()) return;
        Inventory.ChangeWeapon(true);
    }
    
    protected virtual void SwapWeaponDown()
    {
        if(!Inventory.HasCurrentWeapon()) return;
        Inventory.ChangeWeapon(false);
    }
    
    public virtual void TakeDamage(int amount)
    {
        Damageable.TakeDamage(amount);
    }

    public virtual void AddHealth(int amount)
    {
        Damageable.AddHealth(amount);
    }
}
