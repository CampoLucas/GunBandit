using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : StatsSO
{
    [Header("Stats")]
    [Range(0.0001f, 400)][SerializeField] private float damage = 100f;
    [Range(0.0001f, 50)][SerializeField] private float range = 5f;
    [Range(0.0001f, 50)][SerializeField] private float throwStrength = 5f;
    
    [Header("Rigidbody")]
    [Range(0.0001f, 20)][SerializeField] private float mass = 1;
    [SerializeField] private float linearDrag = 0;
    
    [Header("Collider")]
    [SerializeField] private Vector2 offset;
    [SerializeField] private Vector2 size;
    
    [Header("Hand Position")]
    [SerializeField] private Vector2 handPos;
    public float Damage => damage;
    public float Range => range;
    public float ThrowStrength => throwStrength;
    public float Mass => mass;
    public float LinearDrag => linearDrag;
    public Vector2 Offset => offset;
    public Vector2 Size => size;

    public Vector3 Position(Vector3 position) =>
        new Vector3(position.x + handPos.x, position.y + handPos.y);

}
