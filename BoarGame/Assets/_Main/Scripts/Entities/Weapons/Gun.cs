using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic gun
/// </summary>
public class Gun : Weapon
{
    /// <summary>
    /// Fire the gun
    /// </summary>
    public void Fire()
    {
        
    }

    /// <summary>
    /// Reload the gun
    /// </summary>
    public void Reload()
    {
        
    }

    /// <summary>
    /// Use the gun as a melee weapon
    /// </summary>
    public override void Attack()
    {
        base.Attack();
    }

    /// <summary>
    /// Throw the gun
    /// </summary>
    public override void Throw()
    {
        base.Throw();
    }
}
