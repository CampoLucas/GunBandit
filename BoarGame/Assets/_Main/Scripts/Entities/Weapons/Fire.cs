using System;
using UnityEngine;

public class Fire : MonoBehaviour, IAttack, IFactory<Entity, StatsSO>
{
    private GunSO _stats;
    private IReloadable _reloadable;
    private float _lastFiredTime;
    
    [SerializeField] private Entity bulletPrefab;
    [SerializeField] private Transform bulletSpawnPos;
    
    public Entity Product => bulletPrefab;

    private void Awake()
    {
        _stats = GetComponent<Ranged>().GetData() as GunSO;
        _reloadable = GetComponent<Reloadable>();
    }

    public void Attack()
    {
        if (_reloadable.OutOfAmmo() || _reloadable.IsReloading()) return;
        
        if (!(_lastFiredTime + _stats.FireRate < Time.time)) return;
        _lastFiredTime = Time.time;
        Create();
        _reloadable.DecreaseAmmo();
    }

    
    public Entity Create()
    {
        Entity e = Instantiate(Product, bulletSpawnPos.position, Quaternion.identity);
        e.transform.rotation = transform.rotation;
        
        return e;
    }

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
