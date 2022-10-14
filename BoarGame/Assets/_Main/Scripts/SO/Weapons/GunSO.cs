using System;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu(fileName = "Gun", menuName = "Entities/Stats/Weapons/Gun", order = 0)]
public class GunSO : WeaponSO
{
    [SerializeField] private ParticleSystem muzzle;

    [SerializeField] private float fireRate = 1.5f;
    
    [SerializeField] private float reloadSpeed = 1f;

    [SerializeField] private int ammo = 60;
    [SerializeField] private int magAmmo = 6;
    
    
    [SerializeField] private int pellets = 5;
    [SerializeField] private float spread = 15f;

    [SerializeField] private bool hasRecoil = true;
    [SerializeField] private float recoil = 0.5f;
    [SerializeField] private float snappiness = .2f;
    [SerializeField] private float returnSpeed = .2f;
    
    [SerializeField] private BulletSO bulletData;
    [SerializeField] private Bullet bulletPrefab;

    public ParticleSystem Muzzle => muzzle;
    public float FireRate => fireRate;
    public float ReloadSpeed => reloadSpeed;
    public int Ammo => ammo;
    public int MagAmmo => magAmmo;
    public int Pellets => pellets;
    public float Spread => spread;
    public bool HasRecoil => hasRecoil;
    public float Recoil => recoil;
    public float Snappiness => snappiness;
    public float ReturnSpeed => returnSpeed;
    public BulletSO BulletData => bulletData;
    public Bullet BulletPrefab => bulletPrefab;
}
