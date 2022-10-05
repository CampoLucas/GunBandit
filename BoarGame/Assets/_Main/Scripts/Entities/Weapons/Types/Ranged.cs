using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Weapon2
{
    public IReloadable Reloadable { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Reloadable = GetComponent<IReloadable>();
    }

    // public override void Attack()
    // {
    //     if (Reloadable.OutOfAmmo() || Reloadable.IsReloading()) return;
    //     base.Attack();
    //     Reloadable.DecreaseAmmo();
    // }

    public void Reload()
    {
        Reloadable.Reload();
    }
}
