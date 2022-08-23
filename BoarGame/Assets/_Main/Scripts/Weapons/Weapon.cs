using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All type weapon inherits this class
/// </summary>
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponSO stats;
    public WeaponSO Data => stats;
    public virtual void Attack()
    {
        
    }
    
    public virtual void Throw()
    {
        
    }
}
