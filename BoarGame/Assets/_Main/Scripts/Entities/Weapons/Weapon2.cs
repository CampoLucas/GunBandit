using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : Entity, IWeapon
{
    private IThrowable _throwable;
    private IAttack _attack;
    private ISwapState _swap;
    private Rigidbody2D _rigidbody;

    public WeaponState CurrentState => _swap.CurrentState;

    protected override void Awake()
    {
        base.Awake();
        _throwable = GetComponent<IThrowable>();
        _attack = GetComponent<IAttack>();
        _swap = GetComponent<ISwapState>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        base.Start();
        InitStats();
    }

    protected override void InitStats()
    {
        base.InitStats();
        var weapon = GetData() as WeaponSO;
        if (!weapon) return;
        _rigidbody.sharedMaterial = weapon.PhysicsMaterial;
        _rigidbody.mass = weapon.Mass;
        _rigidbody.drag = weapon.Drag;
    }
    
    public virtual void Attack()
    {
        _attack.Attack();
    }

    public virtual void Throw()
    {
        ChangeState(WeaponState.Thrown);
        _throwable.Throw();
    }

    public void ChangeState(WeaponState state)
    {
        _swap.ChangeState(state);
    }
}
