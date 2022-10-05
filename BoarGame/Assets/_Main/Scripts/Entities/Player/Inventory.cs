using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct InventoryWeapon
{
    public WeaponSO Data;
    public Weapon2 Weapon;

}

public class Inventory : MonoBehaviour
{
    private List<InventoryWeapon> _weaponList;
    private Animator _animator;
    
    [SerializeField] private int _itemIndex;
    [SerializeField] private int maxWeaponCapacity = 2;
    [SerializeField] private Transform handPos;
    
    public Action<Weapon2> OnWeaponChange;
    
    public Weapon2 CurrentWeapon()
    {
        if (_weaponList.Count > 0)
            return _weaponList[_itemIndex].Weapon;
        else
            return null;
    }
    

    private void Awake()
    {
         _weaponList = new List<InventoryWeapon>();
        for (int i = 0; i < _weaponList.Count; i++)
        {
            AddItem(GetComponentsInChildren<Pickable>()[i]);
        }
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (CurrentWeapon() != null) CurrentWeapon().ChangeState(WeaponState.Equipped);
        _itemIndex = 0;
    }

    public void AddItem(Pickable itemToPickup)
    {
        if(HasFullInventory()) return;
        var data = itemToPickup.GetData() as WeaponSO;
        var weapon = itemToPickup.GetComponent<Weapon2>();
        _weaponList.Add(new InventoryWeapon()
        {
            Data = data,
            Weapon = weapon
        });
        
        weapon.ChangeState(WeaponState.Equipped);
        
        
        
        //Changes weapon position
        var item = itemToPickup.gameObject;

        if (_weaponList.Count > 1)
        {
            item.SetActive(false);
        }
        else
        {
            OnWeaponChange?.Invoke(weapon);
            //Todo: IAnimator
            _animator.SetFloat("WeaponType", (int)data.Type);
            // item.transform.parent = transform;
            // item.transform.rotation = handPos.transform.rotation;
            // item.transform.position = handPos.transform.position;
        }
        
    }

    public void DropItem()
    {
        _weaponList.Remove(_weaponList[_itemIndex]);
        if (_weaponList.Count > 0)
        {
            var item = CurrentWeapon().gameObject;
            item.SetActive(true);
           
            //Todo: IAnimator
            _animator.SetFloat("WeaponType", (int)_weaponList[_itemIndex].Data.Type);
            OnWeaponChange?.Invoke(_weaponList[_itemIndex].Weapon);
        }
        else
        {
            //Todo: IAnimator
            _animator.SetFloat("WeaponType", 0);
        }
    }

    public bool HasFullInventory()
    {
         return _weaponList.Count >= maxWeaponCapacity;
    }

    //Todo: IChangePosition
    public void ChangeWeaponPosition()
    {
        var item = CurrentWeapon().gameObject;
        item.transform.parent = transform;
        item.transform.rotation = handPos.transform.rotation;
        item.transform.position = handPos.transform.position;
    }

    
}
