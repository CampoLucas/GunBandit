using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class WeaponDisplay : MonoBehaviour
{

    [SerializeField] private Player player;
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text magazine;
    [SerializeField] private TMP_Text ammo;

    private void Update()
    {
        //if(player.Inventory.CurrentWeapon() == null) return;
        //var weapon = player.Inventory.CurrentWeapon();
        //ChangeIcon(weapon);
        //UpdateBullets(weapon);
    }

    private void ChangeIcon(Weapon2 currentWeapon)
    {
        var data = currentWeapon ? currentWeapon.GetData() : null;
        icon.sprite = data ? data.Icon : defaultIcon;
        weaponName.text = data ? data.name : "";
    }

    private void UpdateBullets(Weapon2 currentWeapon)
    {
        var gun = currentWeapon as Ranged;
        magazine.text = gun ? gun.Reloadable.CurrentMagAmmo.ToString() : "";
        ammo.text = gun ? gun.Reloadable.CurrentAmmo.ToString() : "";
    }
}
