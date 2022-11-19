using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloadable : Subject, IReloadable
{
    private GunSO _stats;
    private bool _isReloading;
    private List<Observer> _subscribers = new List<Observer>();
    public override List<Observer> Subscribers => _subscribers;
    public int CurrentAmmo { get; private set; }
    public int CurrentMagAmmo { get; private set; }

    private void Awake()
    {
        _stats = GetComponent<IWeapon>().GetData() as GunSO;
        
    }
    private void Start()
    {
        CurrentAmmo = _stats.Ammo - _stats.MagAmmo;
        CurrentMagAmmo = _stats.MagAmmo;
        Subscribe(GetComponentInChildren<Observer>());
    }

    private void Update()
    {
        if (CurrentMagAmmo <= 0)
            Reload();
    }
    private void OnEnable()
    {
        _isReloading = false;
    }

    public void Reload()
    {
        if (!IsReloading() && CurrentAmmo > 0)
            StartCoroutine(ReloadCoroutine());
        if (CurrentAmmo <= 0)
            NotifyAll("OUT_OF_AMMO");
    }

    public bool OutOfAmmo() => CurrentAmmo <= 0 && CurrentMagAmmo <= 0;
    public bool IsReloading() => _isReloading;
    
    // _currentAmmo += amount;
    // if(_currentAmmo > _stats.Ammo)
    //     _currentAmmo = _stats.Ammo;
    public void GetAmmo(in int amount) => CurrentAmmo = CurrentAmmo >= _stats.Ammo ? CurrentAmmo = _stats.Ammo : CurrentAmmo += amount;
    public void DecreaseAmmo() => CurrentMagAmmo--;
    public void DecreaseAmmo(in int amount) => CurrentMagAmmo -= amount;
    
    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        NotifyAll("RELOADING");
        yield return new WaitForSeconds(_stats.ReloadSpeed);

        //If there is a bullet in the left in the mag, after reloading you will have an extra bullet
        if (CurrentAmmo > _stats.MagAmmo)
            CurrentMagAmmo = CurrentMagAmmo != 1 ? _stats.MagAmmo : _stats.MagAmmo + 1;
        else
            CurrentMagAmmo = CurrentMagAmmo != 1 ? CurrentAmmo : CurrentAmmo + 1;
        
        //If the amout of ammo per mag is greater than the amount of ammo left, currentAmmo equals 0
        CurrentAmmo = CurrentAmmo > _stats.MagAmmo ? CurrentAmmo - _stats.MagAmmo : 0;

        _isReloading = false;
        NotifyAll("RELOADED");
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
