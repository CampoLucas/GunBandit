using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private Rigidbody2D _rigidbody;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.AddForce(transform.up * 15, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
