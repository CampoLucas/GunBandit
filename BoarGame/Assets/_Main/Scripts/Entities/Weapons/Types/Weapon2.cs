using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : Entity, IWeapon
{
    private IThrowable _throwable;
    private IAttack _attack;
    private ISwapState _swap;

    protected virtual void Awake()
    {
        _throwable = GetComponent<IThrowable>();
        _attack = GetComponent<IAttack>();
        _swap = GetComponent<ISwapState>();
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

    public void Sheath(in bool isSheath)
    {
        gameObject.SetActive(isSheath);
    }

    public void ChangeState(WeaponState state)
    {
        _swap.ChangeState(state);
    }
}
