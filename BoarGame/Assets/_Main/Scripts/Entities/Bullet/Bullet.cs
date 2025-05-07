using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Bullet : Entity
{
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private BulletSO _stats;
    private Vector2 _dir;
    private float _lifeTimer;
    private BulletPool _pool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _lifeTimer = _stats != null ? _stats.Range : 2f;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0f;
    }

    private void Update()
    {
        _lifeTimer -= Time.deltaTime;
        if (_lifeTimer <= 0f)
            _pool.ReturnBullet(this); // Devolver al pool
    }

    public void InitStats(BulletSO data, Vector2 dir)
    {
        _stats = data;
        _sprite.sprite = data.Sprite;
        _dir = dir;

        _rigidbody.AddForce(_dir * _stats.Force, ForceMode2D.Impulse);
    }

    public void InitStats(BulletSO data, Vector2 dir, string bulletTag)
    {
        InitStats(data, dir);
        tag = bulletTag;
    }

    public void SetPool(BulletPool pool)
    {
        _pool = pool;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(tag))
        {
            var character = other.gameObject.GetComponent<Character>();
            if (character)
                character.TakeDamage(_stats.Damage);
        }

        _pool.ReturnBullet(this); // Devolver al pool
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si usás triggers, aplicá lo mismo
    }
}
