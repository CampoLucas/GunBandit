using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : StatsSO
{
    [Header("Physics")] 
    [SerializeField] private RigidbodyData rigidbody;
    
    [Header("Weapon specs")]
    [SerializeField] private WeaponType type = WeaponType.Empty;
    
    [Header("Stats")]
    [Range(0.0001f, 50)][SerializeField] private float throwStrength = 5f;
    
    
    public float ThrowStrength => throwStrength;
    
    public float Mass => rigidbody.Mass;
    public float LinearDrag => rigidbody.LinearDrag;
    public WeaponType Type => type;

}

public enum WeaponType { Empty, Pistol, Shotgun}

[System.Serializable]
public struct RigidbodyData
{
    [Range(0.0001f, 20)][SerializeField] private float mass;
    [SerializeField] private float linearDrag;

    public float Mass => mass;
    public float LinearDrag => linearDrag;
}
