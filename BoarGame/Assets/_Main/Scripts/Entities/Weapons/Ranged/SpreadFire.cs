using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpreadFire : Fire
{
    public override void Attack()
    {
        //if (Reloadable.OutOfAmmo() || Reloadable.IsReloading()) return;
        
        if (!(LastFiredTime + Stats.FireRate < Time.time)) return;
        LastFiredTime = Time.time;

        Muzzle.Play();
        for (int i = 0; i < Stats.Pellets; i++)
        {
            var pellet = Create();
            var targetRot = Quaternion.RotateTowards(BulletSpawnPos.rotation, Random.rotation, Stats.Spread);
            var targetUp = targetRot * Vector3.up;
            pellet.transform.rotation = targetRot;
            pellet.InitStats(Stats.BulletData, targetUp);
        }
        
        Reloadable.DecreaseAmmo();
    }
}
