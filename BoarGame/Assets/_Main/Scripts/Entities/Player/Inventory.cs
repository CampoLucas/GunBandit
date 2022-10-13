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
    private Player _player;
    
    [SerializeField] private Transform handTransform;
    [SerializeField] private int _itemIndex;
    
    [SerializeField] private int maxWeaponCapacity = 2;
    
    public Weapon2 CurrentWeapon()
    {
        if (_weaponList.Count > 0)
        {
            // if (_itemIndex > _weaponList.Count)
            //     _itemIndex = _weaponList.Count;
            return _weaponList[_itemIndex].Weapon;
        }
        return null;
    }
    

    private void Awake()
    {
        _weaponList = new List<InventoryWeapon>();
        _player = GetComponent<Player>();
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
            weapon.gameObject.transform.parent = handTransform;
            item.SetActive(false);
        }
        else
        {
            //Todo: IAnimator
            _animator.SetFloat("WeaponType", (int)data.Animation);
            _player.PickUpWeapon();
        }

    }

    public void DropItem()
    {
        var currentIndex = _itemIndex;
        if (_itemIndex > 0)
            _itemIndex--;
        _weaponList.Remove(_weaponList[currentIndex]);
        // if (_itemIndex > _weaponList.Count)
        //     _itemIndex--;
        if (_weaponList.Count > 0)
        {
            if (_itemIndex > _weaponList.Count)
                _itemIndex = _weaponList.Count;
            
            var item = CurrentWeapon().gameObject;
            item.SetActive(true);
            //PickUp(CurrentWeapon());
           
            //Todo: IAnimator
            _animator.SetFloat("WeaponType", (int)_weaponList[_itemIndex].Data.Animation);
            _player.PickUpWeapon();
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

    public bool HasAWeapon()
    {
        return CurrentWeapon();
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
        _animator.SetFloat("WeaponType", (int)_weaponList[_itemIndex].Data.Animation);
        _player.PickUpWeapon();
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
