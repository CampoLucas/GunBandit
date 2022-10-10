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
    private int _itemIndex;
    
    [SerializeField] private int maxWeaponCapacity = 2;
    [SerializeField] private Transform handPos;
    
    public Weapon2 CurrentWeapon()
    {
        if (_weaponList.Count > 0)
            return _weaponList[_itemIndex].Weapon;
        return null;
    }
    

    private void Awake()
    {
        _weaponList = new List<InventoryWeapon>();
        //Chequea si tiene algun arma y la equipa
        for (int i = 0; i < _weaponList.Count; i++)
        {
            AddItem(GetComponentsInChildren<Pickable>()[i]);
        }
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        //Si teiene un arma la cambia de estado
        if (CurrentWeapon() != null) CurrentWeapon().ChangeState(WeaponState.Equipped);
        _itemIndex = 0;
    }

    public void AddItem(Pickable itemToPickup)
    {
        if(HasFullInventory()) return;
        var data = itemToPickup.GetData();
        var weapon = itemToPickup.GetComponent<Weapon2>();
        _weaponList.Add(new InventoryWeapon()
        {
            Data = data,
            Weapon = weapon
        });
        
        weapon.ChangeState(WeaponState.Equipped);
        if (_weaponList.Count > 1)
        {
            var item = itemToPickup.gameObject;
            item.SetActive(false);
        }
        else
        {
            //Todo: IAnimator
            _animator.SetFloat("WeaponType", (int)data.Type);
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

    public void ChangeWeapon(bool up)
    {
        
        //_animator.SetFloat("WeaponType", (int)_weaponList[_itemIndex].Data.Type);
        _itemIndex = up ? _itemIndex + 1 : _itemIndex - 1;
        if (up && _itemIndex > _weaponList.Count - 1)
            _itemIndex = 0;
        
        if (!up && _itemIndex < 0)
            _itemIndex = _weaponList.Count - 1;
        
        SetValues();
    }

    private void SetValues()
    {
        SlotOrder();
        DisableWeapon();
        _animator.SetFloat("WeaponType", (int)_weaponList[_itemIndex].Data.Type);
    }

    private void SlotOrder()
    {
        for (var i = 0; i < _weaponList.Count; i++)
        {
            if (!_weaponList[i].Weapon.gameObject)
                _itemIndex++;
        }
    }

    private void DisableWeapon()
    {
        for (var i = 0; i < _weaponList.Count; i++)
        {
            _weaponList[i].Weapon.gameObject.SetActive(i == _itemIndex);
        }
    }
}
