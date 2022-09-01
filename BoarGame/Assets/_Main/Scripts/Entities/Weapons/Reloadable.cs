using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reloadable : MonoBehaviour
{
    private GunSO _stats;
    private int _currentAmmo;
    private int _currentMagAmmo;
    private bool _isReloading;
    public int CurrentAmmo => _currentAmmo;
    public int CurrentMagAmmo => _currentMagAmmo;

    private void Awake()
    {
        _stats = GetComponent<Gun>().GetData() as GunSO;
    }
    private void Start()
    {
        _currentAmmo = _stats.Ammo - _stats.MagAmmo;
        _currentMagAmmo = _stats.MagAmmo;
    }

    private void Update()
    {
        if (_currentMagAmmo <= 0)
            Reload();
    }
    private void OnEnable()
    {
        _isReloading = false;
    }

    public void Reload()
    {
        if (!IsReloading() && _currentAmmo > 0)
            StartCoroutine(ReloadCoroutine());
    }

    private IEnumerator ReloadCoroutine()
    {
        _isReloading = true;

        yield return new WaitForSeconds(_stats.ReloadSpeed);

        //If there is a bullet in the left in the mag, after reloading you will have an extra bullet
        if (_currentAmmo > _stats.MagAmmo)
        {
            _currentMagAmmo = _currentMagAmmo != 1 ? _stats.MagAmmo : _stats.MagAmmo + 1;
        }
        else
        {
            _currentMagAmmo = _currentMagAmmo != 1 ? _currentAmmo : _currentAmmo + 1;
        }
        //If the amout of ammo per mag is greater than the amount of ammo left, currentAmmo equals 0
        _currentAmmo = _currentAmmo > _stats.MagAmmo ? _currentAmmo - _stats.MagAmmo : 0;

        _isReloading = false;
    }

    /// <summary>
    /// Check if the gun is out of ammo
    /// </summary>
    /// <returns></returns>
    public bool OutOfAmmo()
    {
        return _currentAmmo <= 0 && _currentMagAmmo <= 0;
    }

    /// <summary>
    /// Check if the player is reloading
    /// </summary>
    /// <returns></returns>
    public bool IsReloading()
    {
        return _isReloading;
    }

    /// <summary>
    /// Add bullets; It won't go higger than the max amount of ammo
    /// </summary>
    /// <param name="amount"></param>
    public void GetAmmo(int amount)
    {
        _currentAmmo += amount;
        if(_currentAmmo > _stats.Ammo)
            _currentAmmo = _stats.Ammo;
    }

    /// <summary>
    /// Decrease ammo once
    /// </summary>
    public void DecreaseAmmo()
    {
        _currentMagAmmo--;
    }

    /// <summary>
    /// An ammount to decrease ammo
    /// </summary>
    /// <param name="amount"></param>
    public void DecreaseAmmo(int amount)
    {
        _currentMagAmmo -= amount;
    }
}
