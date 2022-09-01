using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    private WeaponSO _stats;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _stats = GetComponent<Weapon>().GetData() as GunSO;
        _rigidbody = GetComponent<Rigidbody2D>();
    }
}
