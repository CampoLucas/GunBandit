using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private WeaponSO _data;
    private Collider _collider;
    
    private PlayerInputHandler _inputs;
    private IInventory _inventory;

    public WeaponSO GetData() => _data;

    private void Awake()
    {
        _data = GetComponent<Entity>().GetData() as WeaponSO;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        if(_inputs == null)
            _inputs = other.GetComponent<PlayerInputHandler>();
        if(_inventory == null) 
            _inventory = other.GetComponent<Character>().Inventory;
        _inputs.OnInteractPerformed += AddItem;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        _inputs.OnInteractPerformed -= AddItem;
    }

    private void AddItem()
    {
        _inventory.AddItem(this);
    }

}
