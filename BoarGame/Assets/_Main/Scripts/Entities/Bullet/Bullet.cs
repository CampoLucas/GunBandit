using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private Rigidbody2D _rigidbody;
    private BulletSO _stats;
    private Vector2 _dir;
    protected override void Awake()
    {
        base.Awake();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected override void Start()
    {
        _rigidbody.AddForce(_dir * _stats.Force, ForceMode2D.Impulse);
        Destroy(gameObject, _stats.Range);
    }
    
    public void InitStats(BulletSO data, Vector2 dir)
    {
        InitStats();
        _stats = data;
        _dir = dir;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
