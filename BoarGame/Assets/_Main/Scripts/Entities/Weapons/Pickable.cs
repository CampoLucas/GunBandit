using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour
{
    private WeaponSO _data;
    private Collider _collider;

    public WeaponSO GetData() => _data;

    private void Awake()
    {
        _data = GetComponent<Entity>().GetData() as WeaponSO;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.gameObject.CompareTag("Player")) return;
        
        Inventory.Instance.AddItem(this);
    }

}
