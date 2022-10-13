using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private WeaponSO _data;
    private Collider _collider;
    private Inventory _inventory;

    public WeaponSO GetData() => _data;

    private void Awake()
    {
        _data = GetComponent<Entity>().GetData() as WeaponSO;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        var player = other.GetComponent<Player>();
        if(!player) return;
        if (!_inventory)
            _inventory = player.Inventory;
        player.Input.OnInteractPerformed += AddItem;
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        var player = other.GetComponent<Player>();
        if(!player) return;
        player.Input.OnInteractPerformed -= AddItem;
    }

    private void AddItem()
    {
        _inventory.AddItem(this);
    }

}
