using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : StatsSO
{
    [SerializeField] private float damage = 100f;
    [SerializeField] private float range = 5f;
    [SerializeField] private float throwStrength = 5f;
    [SerializeField] private float linearDrag = 0;
    [SerializeField] private Vector2 handPos;
    public float Damage => damage;
    public float Range => range;
    public float ThrowStrength => throwStrength;
    public float LinearDrag => linearDrag;

    public Vector3 Position(Vector3 position) =>
        new Vector3(position.x + handPos.x, position.y + handPos.y);

}
