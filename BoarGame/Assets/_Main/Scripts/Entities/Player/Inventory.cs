using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;



public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Pickable> weaponList;
    [SerializeField] private int _itemIndex;
    [SerializeField] private int maxWeaponCapacity = 2;
    
    public static Inventory Instance;

    public Weapon CurrentWeapon
    {
        get
        {
            if (weaponList.Count > 0)
                return weaponList[_itemIndex].GetComponent<Weapon>();
            else
                return null;
        }
    }
    

    private void Awake()
    {
        //if(Instance == null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        Instance = this;

        weaponList = new List<Pickable>();
        

        for (int i = 0; i < weaponList.Count; i++)
        {
            AddItem(GetComponentsInChildren<Pickable>()[i]);
        }
    }

    private void Start()
    {
        if (CurrentWeapon != null) CurrentWeapon.ChangeState(WeaponState.Equipped);
        _itemIndex = 0;
    }

    public void AddItem(Pickable itemToPickup)
    {
        if(HasFullInventory()) return;
        var data = itemToPickup.GetData() as WeaponSO;
        
        weaponList.Add(itemToPickup);
        
        itemToPickup.GetComponent<Weapon>().ChangeState(WeaponState.Equipped);
        
        var item = itemToPickup.gameObject;
        item.transform.parent = transform;
        item.transform.rotation = transform.rotation;
        if (data != null) item.transform.position = data.Position(transform.position);
    }
    public void GetItem(Pickable itemToPickup)
    {
        //if(HasFullInventory()) return;
        

        // var data = itemToPickup.GetData();
        //
        // if(HasFullInventory()) return;
        //
        // _itemDictionary.Add(new InventoryItem()
        // {
        //     Item = data
        // });
        //
        // itemToPickup.GetComponent<Weapon>().ChangeState(WeaponState.Equipped);
        // var item = itemToPickup.gameObject;
        // item.transform.parent = transform;
        // item.transform.rotation = transform.rotation;
        // item.transform.position = data.Position(transform.position);

        // If players has a weapon it should set active false.
    }

    public bool HasFullInventory()
    {
         return weaponList.Count >= maxWeaponCapacity;
    }

    
}
