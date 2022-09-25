using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon2 : Entity, IWeapon
{
    private IThrowable _throwable;
    private IAttack _attack;

    protected override void Awake()
    {
        base.Awake();
        _throwable = GetComponent<IThrowable>();
        _attack = GetComponent<IAttack>();
    }
    
    public virtual void Attack()
    {
        _attack.Attack();
    }

    public virtual void Throw()
    {
        _throwable.Throw();
    }

    public void Sheath(in bool isSheath)
    {
        gameObject.SetActive(isSheath);
    }
}
