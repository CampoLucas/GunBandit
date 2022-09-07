using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tiene los datos del objeto y la cantidad de stock
/// </summary>
public struct InventoryItem
{
    public WeaponSO Item;
}

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    /// <summary>
    /// Diccionario para poder correlacionar
    /// El string se guarda en un scriptable object que me dice que item es
    /// y el item del inventario 
    /// </summary>
    private Dictionary<string, InventoryItem> _itemDictionary;
    private int _maxItemCapacity = 2;

    private void Awake()
    {
        //if(Instance == null)
        //{
        //    Destroy(gameObject);
        //    return;
        //}

        Instance = this;

        _itemDictionary = new Dictionary<string, InventoryItem>();
    }

    public void GetItem(Pickable itemToPickup)
    {
        var data = itemToPickup.GetData();
        
        if(HasFullInventory()) return;
        
        _itemDictionary.Add(data.Id, new InventoryItem()
        {
            Item = data
        });

        itemToPickup.GetComponent<Weapon>().ChangeState(WeaponState.Equipped);
        var item = itemToPickup.gameObject;
        item.transform.parent = transform;
        item.transform.rotation = transform.rotation;
        item.transform.position = data.Position(transform.position);
        
        // If players has a weapon it should set active false.
    }

    public bool HasFullInventory()
    {
        return _itemDictionary.Count >= _maxItemCapacity;
    }
}
