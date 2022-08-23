using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : StatsSO
{
    [SerializeField] private float damage = 100f;
    [SerializeField] private float range = 5f;
    public float Damage => damage;
    public float Range => range;
}
