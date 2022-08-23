using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All type weapon inherits this class
/// </summary>
public class Weapon : Entity
{
    public virtual void Attack()
    {
        
    }
    
    public virtual void Throw()
    {
        // Stops being child
        Move(transform.up);
    }
}
