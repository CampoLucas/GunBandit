using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : Weapon2
{
    private Recoil _recoil;
    public IReloadable Reloadable { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Reloadable = GetComponent<IReloadable>();
        _recoil = GetComponent<Recoil>();
    }

    public override void Attack()
    {
        base.Attack();
        if(_recoil)
            _recoil.RecoilFire();
    }

    public void Reload()
    {
        Reloadable.Reload();
    }
}
