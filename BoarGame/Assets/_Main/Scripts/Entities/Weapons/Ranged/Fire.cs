using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Fire : MonoBehaviour, IAttack, IFactory<Bullet, StatsSO>
{
    protected GunSO Stats;
    protected IReloadable Reloadable;
    protected float LastFiredTime;
    protected Transform BulletSpawnPos;
    protected ParticleSystem Muzzle;
    private Light2D _light;
    private ChangeLightColor _lightColor;
    
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
        Muzzle = muzzle;
        _light = muzzle.GetComponentInChildren<Light2D>();
        _lightColor = _light.GetComponent<ChangeLightColor>();

    }

    private void Start()
    {
        Muzzle.transform.position = BulletSpawnPos.position;
        _light.enabled = false;
    }

    private void Update()
    {
        _light.enabled = Muzzle.isPlaying;
        if (!Muzzle.isPlaying) return;
        _lightColor.ChangeColor();
    }

    public virtual void Attack()
    {
        if (Reloadable.OutOfAmmo() || Reloadable.IsReloading()) return;
        // if (Stats.Hold)
        // {
        //     if (!(LastFiredTime + Stats.FireRate < Time.time)) return;
        //     LastFiredTime = Time.time;
        // }
        if (!(LastFiredTime + Stats.FireRate < Time.time)) return;
        LastFiredTime = Time.time;
        Muzzle.Play();
        var bullet = Create();
        Reloadable.DecreaseAmmo();
    }

    
    public Bullet Create()
    {
        Bullet e = Instantiate(Product, BulletSpawnPos.position, Quaternion.identity);
        e.gameObject.transform.rotation = transform.rotation;
        e.InitStats(Stats.BulletData, BulletSpawnPos.transform.up);
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
