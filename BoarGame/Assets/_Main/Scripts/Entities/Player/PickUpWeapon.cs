using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpWeapon : MonoBehaviour
{
    private readonly Invoker _invoker = new();
    [SerializeField] private Transform handTransform;
    
    public void PickUp(Weapon2 weapon)
    {
        var pickUp = new CmdPickUpWeapon(weapon, handTransform);
        _invoker.AddCommand(pickUp);
    }
    
    public void Drop()
    {
        _invoker.UndoCommand();
    }
}
