using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Gun,
        Grenade,
        Shield,
        MeleeWeapon
    }

    public ItemType itemType;
}
