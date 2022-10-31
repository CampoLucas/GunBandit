using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponState
{
    Equipped,
    Stored,
    Pickable,
    Thrown
}
public class SwapState : MonoBehaviour, ISwapState
{
    private WeaponSO _stats;
    private Rigidbody2D _rigidbody;
    private CapsuleCollider2D _collider;
    
    [SerializeField] private WeaponState currentState = WeaponState.Pickable;
    //[SerializeField] private Transform handPos;

    public WeaponState CurrentState => currentState;
    public Action OnWeaponChange { get; private set; }

    private void Awake()
    {
        _stats = GetComponent<Weapon2>().GetData() as WeaponSO;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<CapsuleCollider2D>();
        OnWeaponChange += WeaponChange;
        if(currentState != WeaponState.Equipped)
            ChangeState(WeaponState.Pickable);
    }

    private void Start()
    {
        
    }

    public void ChangeState(WeaponState state)
    {
        currentState = state;
        OnWeaponChange?.Invoke();
    }
    
    private void WeaponChange()
    {
        switch (currentState)
        {
            case WeaponState.Equipped:
                Equip();
                
                return;
            case WeaponState.Pickable:
                Pickable();

                return;
            case WeaponState.Thrown:
                Thrown();

                return;
            case WeaponState.Stored:
                Store();
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Equip()
    {
        _collider.isTrigger = false;
        _collider.enabled = false;
                
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Store()
    {
        
    }

    private void Pickable()
    {
        //Equip();
        _collider.enabled = true;
        _collider.isTrigger = true;
                
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Thrown()
    {
        //Equip();
        _collider.enabled = true;
        _collider.isTrigger = false;
                
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.constraints = RigidbodyConstraints2D.None;
        if (_stats != null) _rigidbody.drag = _stats.LinearDrag;
    }
}
