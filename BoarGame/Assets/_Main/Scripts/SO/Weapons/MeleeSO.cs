using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Melee", menuName = "Entities/Weapons/Melee", order = 1)]
public class MeleeSO : WeaponSO
{
    [SerializeField] private float durability;
    public float Durability => durability;
}
