using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour, IAttack, IFactory<Bullet, StatsSO>
{
    private GunSO _stats;
    private IReloadable _reloadable;
    private float _lastFiredTime;
    private Transform _bulletSpawnPos;
    
    public Bullet Product => _stats.BulletPrefab;

    private void Awake()
    {
        _stats = GetComponent<Ranged>().GetData() as GunSO;
        _reloadable = GetComponent<Reloadable>();
        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag($"GunBarrel"))
                _bulletSpawnPos = child.transform;
        }
    }

    public void Attack()
    {
        if (_reloadable.OutOfAmmo() || _reloadable.IsReloading()) return;
        
        if (!(_lastFiredTime + _stats.FireRate < Time.time)) return;
        _lastFiredTime = Time.time;
        var bullet = Create();
        bullet.InitStats(_stats.BulletData, _bulletSpawnPos.transform.up);
        _reloadable.DecreaseAmmo();
    }

    
    public Bullet Create()
    {
        Bullet e = Instantiate(Product, _bulletSpawnPos.position, Quaternion.identity);
        e.transform.rotation = transform.rotation;
        
        return e;
    }

    public Bullet[] Create(in int quantity)
    {
        var bullets = new Bullet[quantity];
        for (var i = 0; i < quantity; i++)
        {
            bullets[i] = Create();
        }
        
        return bullets;
    }
}
