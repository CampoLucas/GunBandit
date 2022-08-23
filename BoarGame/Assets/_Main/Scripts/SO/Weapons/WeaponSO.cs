using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : ScriptableObject
{
    [SerializeField] private string id = "default";
    [SerializeField] private Sprite sprite;
    [SerializeField] private float damage = 100f;
    [SerializeField] private float range = 5f;
    public string Id => id;
    public Sprite Sprite => sprite;
    public float Damage => damage;
    public float Range => range;
}
