using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : Entity, IWeapon
{
    private IThrowable _throwable;
    private IAttack _attack;
    private ISwapState _swap;

    public WeaponState CurrentState => _swap.CurrentState;

    protected virtual void Awake()
    {
        _throwable = GetComponent<IThrowable>();
        _attack = GetComponent<IAttack>();
        _swap = GetComponent<ISwapState>();
    }

    private void InitStats(WeaponSO data)
    {
        
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
