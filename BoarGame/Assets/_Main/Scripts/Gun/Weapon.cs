using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All type weapon inherits this class
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] protected Sprite sprite;
    [SerializeField] protected float damage;
    [SerializeField] protected float range;
    public virtual void Attack()
    {
        
    }
    
    public virtual void Throw()
    {
        
    }
}
