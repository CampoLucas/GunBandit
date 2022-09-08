using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Entities/Weapons/Gun", order = 0)]
public class GunSO : WeaponSO
{
    [Header("Shooting stats")]
    [Range(0.0001f, 20)][SerializeField] private float fireRate = 0.1f;
    [Range(0.0001f, 20)][SerializeField] private float accuracy = 0.5f;
    
    [Header("Reloading stats")]
    [Range(0, 500)][SerializeField] private int ammo = 60;
    [Range(0, 20)][SerializeField] private int magAmmo = 6;
    [Range(0.0001f, 20)][SerializeField] private float reloadSpeed = 1f;
    
    [Header("Gun specs")]
    [SerializeField] private FireMode mode = FireMode.OneTap;
    [SerializeField] private AmmoType type = AmmoType.Pistol;
    public float FireRate => fireRate;
    public float ReloadSpeed => reloadSpeed;
    public float Accuracy => accuracy;
    public int Ammo => ammo;
    public int MagAmmo => magAmmo;
    public FireMode Mode => mode;
    public AmmoType Type => type;
}
public enum FireMode { OneTap, Automatic, Burst }
public enum AmmoType { Shotgun, Pistol, Riffle, Grenade, Electric }
