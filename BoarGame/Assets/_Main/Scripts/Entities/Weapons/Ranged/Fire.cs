using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Fire : Subject, IAttack, IFactory<Bullet, StatsSO>
{
    protected GunSO Stats;
    protected IReloadable Reloadable;
    protected float LastFiredTime;
    protected Transform BulletSpawnPos;
    protected ParticleSystem Muzzle;
    private Light2D _light;
    private ChangeLightColor _lightColor;
    private List<Observer> _subscribers = new List<Observer>();
    private SoundController _sound;
    private BulletPool _bulletPool;

    public override List<Observer> Subscribers => _subscribers;
    public Bullet Product => Stats.BulletPrefab;

    private void Awake()
    {
        Stats = GetComponent<Ranged>().GetData() as GunSO;
        Reloadable = GetComponent<Reloadable>();

        foreach (Transform child in gameObject.transform)
        {
            if (child.CompareTag("GunBarrel"))
                BulletSpawnPos = child.transform;
        }

        if (Stats == null) return;
        var muzzle = Instantiate(Stats.Muzzle, BulletSpawnPos);
        Muzzle = muzzle;
        _light = muzzle.GetComponentInChildren<Light2D>();
        _lightColor = _light.GetComponent<ChangeLightColor>();
        _sound = GetComponent<SoundController>();

        // Buscar el BulletPool
        _bulletPool = GetComponent<BulletPool>();
    }

    private void Start()
    {
        Muzzle.transform.position = BulletSpawnPos.position;
        _light.enabled = false;
        if (_sound)
            Subscribe(_sound);
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
        if (!(LastFiredTime + Stats.FireRate < Time.time)) return;

        LastFiredTime = Time.time;
        Muzzle.Play();
        NotifyAll("FIRE");

        Create(); // Usamos el pool
        Reloadable.DecreaseAmmo();
    }

    public Bullet Create()
    {
        Bullet bullet = _bulletPool.GetBullet();
        bullet.transform.position = BulletSpawnPos.position;
        bullet.transform.rotation = transform.rotation;
        bullet.InitStats(Stats.BulletData, BulletSpawnPos.transform.up);
        return bullet;
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

    public override void Subscribe(Observer observer)
    {
        if (_subscribers.Contains(observer)) return;
        _subscribers.Add(observer);
    }

    public override void Unsubscribe(Observer observer)
    {
        if (_subscribers.Contains(observer)) return;
        _subscribers.Remove(observer);
    }

    public override void NotifyAll(string message, params object[] args)
    {
        foreach (var t in _subscribers)
            t.OnNotify(message, args);
    }
}
