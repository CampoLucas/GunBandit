using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    /// <summary>
    /// Returns the stats
    /// </summary>
    /// <returns></returns>
    StatsSO GetData();
    /// <summary>
    /// Attack method
    /// (Guns: fire)
    /// (Grenades: removes the pin)
    /// (Melee: melee attack)
    /// (Shield: melee attack)
    /// </summary>
    void Attack();
    /// <summary>
    /// Throws weapon
    /// </summary>
    void Throw();
    
}
