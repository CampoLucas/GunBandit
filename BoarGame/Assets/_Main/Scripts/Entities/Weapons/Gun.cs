using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic gun
/// </summary>
public class Gun : Weapon, IFactory<Entity, StatsSO>
{
    private GunSO _stats;
    [SerializeField] private Entity bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;
    private float _lastFiredTime;
    private int _currentAmmo;
    private int _currentMagAmmo;
    private bool _isReloading;
    public Entity Product => bulletPrefab;
    public int CurrentAmmo => _currentAmmo;
    public int CurrentMagAmmo => _currentMagAmmo;

    protected override void Awake()
    {
        base.Awake();
        _stats = Data as GunSO;
    }

    protected override void Start()
    {
        base.Start();
        InitStats();
    }
    private void InitStats()
    {
        _currentAmmo = _stats.Ammo - _stats.MagAmmo;
        _currentMagAmmo = _stats.MagAmmo;
    }

    private void Update()
    {
        if(_currentMagAmmo <= 0)
            Reload();
    }

    private void OnEnable()
    {
        _isReloading = false;
    }



    /// <summary>
    /// Fire the gun
    /// </summary>
    public void Fire()
    {
        if ((_currentAmmo <= 0 && _currentMagAmmo <= 0) || _isReloading) return;
        
        if (!(_lastFiredTime + _stats.FireRate < Time.time)) return;
        _lastFiredTime = Time.time;
        Create();
        _currentMagAmmo --;
    }

    /// <summary>
    /// Reload the gun
    /// </summary>
    public void Reload()
    {
        if (!_isReloading && _currentAmmo > 0 && _currentAmmo > 0)
            StartCoroutine(ReloadCoroutine());
    }
    
    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_stats.ReloadSpeed);

        //If there is a bullet in the left in the mag, after reloading you will have an extra bullet
        if(_currentAmmo > _stats.MagAmmo)
            _currentMagAmmo = _currentMagAmmo != 1 ? _stats.MagAmmo : _stats.MagAmmo + 1;
        else
            _currentMagAmmo = _currentMagAmmo != 1 ? _currentAmmo : _currentAmmo + 1;
        //If the amout of ammo per mag is greater than the amount of ammo left, currentAmmo equals 0
        _currentAmmo = _currentAmmo > _stats.MagAmmo ? _currentAmmo - _stats.MagAmmo : 0;

        _isReloading = false;
    }

    /// <summary>
    /// Use the gun as a melee weapon
    /// </summary>
    public override void Attack()
    {
        base.Attack();
    }

    /// <summary>
    /// Throw the gun
    /// </summary>
    public override void Throw()
    {
        base.Throw();
    }

    /// <summary>
    /// Creates an instance of the bullet
    /// </summary>
    /// <returns></returns>
    public Entity Create()
    {
        Entity e = Instantiate(Product, bulletSpawnPos.position, Quaternion.identity);
        e.transform.rotation = transform.rotation;
        return e;
    }

    /// <summary>
    /// Creates multiples bullets
    /// </summary>
    /// <param name="quantity"></param>
    /// <returns></returns>
    public Entity[] Create(in int quantity)
    {
        var entities = new Entity[quantity];

        for (var i = 0; i < quantity; i++)
        {
            entities[i] = Create();
        }
        return entities;
    }
}
