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
    private bool _isReloading;
    public Entity Product => bulletPrefab;

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

    private void Update()
    {
        if(_currentAmmo <= 0 && !_isReloading)
            StartCoroutine(Reload());
    }

    private void InitStats()
    {
        _currentAmmo = _stats.Ammo;
    }


    /// <summary>
    /// Fire the gun
    /// </summary>
    public void Fire()
    {
        if (!(_lastFiredTime + _stats.FireRate < Time.time)) return;
        _lastFiredTime = Time.time;
        if (_isReloading) return;
        Create();
        _currentAmmo --;
    }

    /// <summary>
    /// Reload the gun
    /// </summary>
    private IEnumerator Reload()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_stats.ReloadSpeed);
        _currentAmmo = _stats.Ammo;

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
