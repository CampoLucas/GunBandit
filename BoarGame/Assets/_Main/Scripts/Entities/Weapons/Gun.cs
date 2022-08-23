using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generic gun
/// </summary>
public class Gun : Weapon, IFactory<Entity, StatsSO>
{
    [SerializeField] private Entity bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;
    public Entity Product => bulletPrefab;
    
    
    /// <summary>
    /// Fire the gun
    /// </summary>
    public void Fire()
    {
        Create();
    }

    /// <summary>
    /// Reload the gun
    /// </summary>
    public void Reload()
    {
        
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
        Entity e = Instantiate(Product, transform.position, Quaternion.identity);
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
