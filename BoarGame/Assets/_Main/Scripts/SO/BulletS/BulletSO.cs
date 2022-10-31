using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Entities/Stats/Bullet", order = 1)]
public class BulletSO : StatsSO
{
    [SerializeField] private float force;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    public float Force => force;
    public float Range => range;
    public int Damage => damage;
}
