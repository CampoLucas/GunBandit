using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text ammo;

    private void Update()
    {
        var gun = player.Weapon as Gun;
        ammo.text = gun != null ? gun.CurrentMagAmmo.ToString() + "/" + gun.CurrentAmmo.ToString() : "";
    }
}
