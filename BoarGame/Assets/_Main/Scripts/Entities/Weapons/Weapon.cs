using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// All type weapon inherits this class
/// </summary>
public class Weapon : Entity
{
    private Rigidbody2D _rigidbody;
    private BoxCollider2D _collider;

    protected override void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Start()
    {
        _collider.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public virtual void Attack()
    {
        
    }
    
    public virtual void Throw()
    {
        var stats = Data as WeaponSO;
        transform.parent = null;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.drag = stats.LinearDrag;
        _rigidbody.AddForce(transform.up * stats.ThrowStrength, ForceMode2D.Impulse);
        _collider.enabled = true;
    }
}
