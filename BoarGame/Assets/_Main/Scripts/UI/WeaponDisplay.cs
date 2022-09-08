using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WeaponDisplay : MonoBehaviour
{
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text magazine;
    [SerializeField] private TMP_Text ammo;

    private void Update()
    {
        var weapon = Inventory.Instance.CurrentWeapon;
        ChangeIcon(weapon);
        UpdateBullets(weapon);
    }

    private void ChangeIcon(Weapon currentWeapon)
    {
        var data = currentWeapon ? currentWeapon.GetData() : null;
        icon.sprite = data ? data.Icon : defaultIcon;
        weaponName.text = data ? data.name : "";
    }

    private void UpdateBullets(Weapon currentWeapon)
    {
        var gun = currentWeapon as Gun;
        magazine.text = gun ? gun.Reloadable.CurrentMagAmmo.ToString() : "";
        ammo.text = gun ? gun.Reloadable.CurrentAmmo.ToString() : "";
    }
}
