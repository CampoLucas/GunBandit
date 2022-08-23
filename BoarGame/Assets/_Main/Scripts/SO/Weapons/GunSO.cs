using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Weapons/Gun", order = 0)]
public class GunSO : WeaponSO
{
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private float reloadSpeed = 0.5f;
    [SerializeField] private float accuracy = 0.5f;
    [SerializeField] private int ammo = 60;
    [SerializeField] private FireMode mode = FireMode.OneTap;
    [SerializeField] private AmmoType type = AmmoType.Pistol;
    public float FireRate => fireRate;
    public float ReloadSpeed => reloadSpeed;
    public float Accuracy => accuracy;
    public int Ammo => ammo;
    public FireMode Mode => mode;
    public AmmoType Type => type;
}
public enum FireMode { OneTap, Automatic, Burst }
public enum AmmoType { Shotgun, Pistol, Riffle, Grenade, Electric }
