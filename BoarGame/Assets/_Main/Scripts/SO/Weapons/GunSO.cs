using UnityEngine;

[CreateAssetMenu(fileName = "Gun", menuName = "Entities/Stats/Weapons/Gun", order = 0)]
public class GunSO : WeaponSO
{
    [Header("Bullet data")]
    [SerializeField] private BulletSO bulletData;
    [SerializeField] private Bullet bulletPrefab;
    
    [Header("Shooting stats")]
    [Range(0.0001f, 20)] [SerializeField] private float fireRate = 0.1f;
    [Range(0, 20)] [SerializeField] private float recoil = 0.5f;
    [Range(0, 20)] [SerializeField] private float snappiness = .2f;
    [Range(0, 20)] [SerializeField] private float returnSpeed = .2f;
    
    [Header("Reloading stats")]
    [Range(0, 500)] [SerializeField] private int ammo = 60;
    [Range(0, 20) ][SerializeField] private int magAmmo = 6;
    [Range(0.0001f, 20)][SerializeField] private float reloadSpeed = 1f;
    
    
    //[Header("Gun specs")]
    //[SerializeField] private FireMode mode = FireMode.OneTap;
    
    public BulletSO BulletData => bulletData;
    public Bullet BulletPrefab => bulletPrefab;
    public float FireRate => fireRate;
    public float ReloadSpeed => reloadSpeed;
    public float Recoil => recoil;
    public float Snappiness => snappiness;
    public float ReturnSpeed => returnSpeed;
    public int Ammo => ammo;
    public int MagAmmo => magAmmo;
    //public FireMode Mode => mode;


}
//public enum FireMode { OneTap, Automatic, Burst }
//public enum AmmoType { Shotgun, Pistol, Riffle, Grenade, Electric }
