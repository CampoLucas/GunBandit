using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic gun
/// </summary>
public class Gun : Weapon, IFactory<Entity, StatsSO>
{
    private Reloadable _reloadable;
    [SerializeField] private Entity bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;
    private float _lastFiredTime;
    public Entity Product => bulletPrefab;
    public Reloadable Reloadable => _reloadable;

    protected override void Awake()
    {
        base.Awake();
        _reloadable = GetComponent<Reloadable>();
    }

    /// <summary>
    /// Fire the gun
    /// </summary>
    public void Fire()
    {
        var stats = GetData() as GunSO;
        if (_reloadable.OutOfAmmo() || _reloadable.IsReloading()) return;
        
        if (!(_lastFiredTime + stats.FireRate < Time.time)) return;
        _lastFiredTime = Time.time;
        Create();
        _reloadable.DecreaseAmmo();
    }

    /// <summary>
    /// Reload the gun
    /// </summary>
    public void Reload()
    {
        _reloadable.Reload();
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
