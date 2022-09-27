using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// All type weapon inherits this class
/// </summary>
public class Weapon : Entity, IWeapon
{
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    
    [SerializeField] private WeaponState _currentState = WeaponState.Pickable;

    /// <summary>
    /// Event that happens when the weapon is changed
    /// </summary>
    public Action OnWeaponChange { get; private set; }

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<CapsuleCollider2D>();
    }

    protected virtual void Start()
    {
        OnWeaponChange += WeaponChange;
        if(_currentState != WeaponState.Equipped)
            ChangeState(WeaponState.Pickable);
    }

    private void FixedUpdate()
    {
        var rbVelocity = new Vector2(Mathf.Abs(_rigidbody.velocity.x), Mathf.Abs(_rigidbody.velocity.y));
        if(_currentState == WeaponState.Thrown && Vector2.Distance(rbVelocity ,Vector2.zero) < 0.4f)
            ChangeState(WeaponState.Pickable);
    }

    public virtual void Attack()
    {
        
    }
    
    /// <summary>
    /// Throws the weapon
    /// </summary>
    public virtual void Throw()
    {
        var stats = GetData() as WeaponSO;
        transform.parent = null;
        ChangeState(WeaponState.Thrown);
        if (stats != null) _rigidbody.AddForce(transform.up * stats.ThrowStrength, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Changes between weapon states
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(WeaponState state)
    {
        _currentState = state;
        OnWeaponChange?.Invoke();
    }

    private void WeaponChange()
    {
        var stats = GetData() as WeaponSO;
        switch (_currentState)
        {
            // The weapon 
            case WeaponState.Equipped:
                _collider.isTrigger = false;
                _collider.enabled = false;
                
                _rigidbody.bodyType = RigidbodyType2D.Kinematic;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                
                return;
            case WeaponState.Pickable:
                _collider.enabled = true;
                _collider.isTrigger = true;
                
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                
                return;
            case WeaponState.Thrown:
                _collider.enabled = true;
                _collider.isTrigger = false;
                
                _rigidbody.bodyType = RigidbodyType2D.Dynamic;
                _rigidbody.constraints = RigidbodyConstraints2D.None;
                if (stats != null) _rigidbody.drag = stats.LinearDrag;
                
                return;
            case WeaponState.Stored:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
}
