using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : StatsSO
{
    [SerializeField] private float damage = 100f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float throwStrength = 5f;
    public float Damage => damage;
    public float Range => range;
    public float ThrowStrength => throwStrength;
}
