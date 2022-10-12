using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Grenade", menuName = "Entities/Stats/Weapons/Grenade", order = 1)]
public class GrenadeSO : WeaponSO
{
    [SerializeField] private float timeToExplode = 3f;
    [SerializeField] private float explosionRange = 2f;
    [SerializeField] private float explosionForce = 200f;
    
    [SerializeField] private LayerMask explosionMask;
    [SerializeField] private ParticleSystem explosionParticles;

    [field: SerializeField] public float Ammmo { get; private set; }
    public float TimeToExplode => timeToExplode;
    public float Range => explosionRange;
    public float Force => explosionForce;
    public LayerMask Mask => explosionMask;
    public ParticleSystem Particles => explosionParticles;

}
