using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic granade
/// </summary>
public class Granade : Weapon
{
    /// <summary>
    /// The weapon destroys itself dealing damage to everything in a range
    /// </summary>
    public void Explode()
    {
        //Instantiate an explosion particle effect
        //Deal damage in a range
        //Destroy itself
    }

    /// <summary>
    /// When you attack using the grenade if it hits something it explodes
    /// </summary>
    public override void Attack()
    {
        base.Attack();
        DetectCollision();
    }
    
    /// <summary>
    /// When thrown it flashes red and after a few seconds explodes
    /// </summary>
    public override void Throw()
    {
        base.Throw();
        //use an IEnumerator for it to explode
    }

    private void DetectCollision()
    {
        //Logic here
        Explode();
    }
}
