using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private BulletSO _stats;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start()
    {
        _rigidbody.AddForce(transform.up * _stats.Force, ForceMode2D.Impulse);
        Destroy(gameObject, _stats.Range);
    }
    
    public void InitStats(BulletSO data)
    {
        _stats = data;
        _sprite.sprite = data.Sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
