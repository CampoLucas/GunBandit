using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour, IAttack, IFactory<Bullet, StatsSO>
{
    protected GunSO Stats;
    protected IReloadable Reloadable;
    protected float LastFiredTime;
    protected Transform BulletSpawnPos;
    protected ParticleSystem Muzzle;
    
    public Bullet Product => Stats.BulletPrefab;

    private void Awake()
    {
        Stats = GetComponent<Ranged>().GetData() as GunSO;
        Reloadable = GetComponent<Reloadable>();
        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag($"GunBarrel"))
                BulletSpawnPos = child.transform;
        }

        if (Stats == null) return;
        var muzzle = Instantiate(Stats.Muzzle, BulletSpawnPos);
        muzzle.transform.position = BulletSpawnPos.position;
        Muzzle = muzzle;
    }

    public virtual void Attack()
    {
        if (Reloadable.OutOfAmmo() || Reloadable.IsReloading()) return;
        
        if (!(LastFiredTime + Stats.FireRate < Time.time)) return;
        LastFiredTime = Time.time;
        Muzzle.Play();
        var bullet = Create();
        bullet.InitStats(Stats.BulletData, BulletSpawnPos.transform.up);
        Reloadable.DecreaseAmmo();
    }

    
    public Bullet Create()
    {
        Bullet e = Instantiate(Product, BulletSpawnPos.position, Quaternion.identity);
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
