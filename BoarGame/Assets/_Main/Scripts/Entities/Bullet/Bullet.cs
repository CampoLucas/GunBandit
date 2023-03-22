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
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }


    private void Start()
    {
        _rigidbody.AddForce(_dir * _stats.Force, ForceMode2D.Impulse);
        Destroy(gameObject, _stats.Range);
    }
    
    public void InitStats(BulletSO data, Vector2 dir)
    {
        _stats = data;
        _sprite.sprite = data.Sprite;
        _dir = dir;
    }

    public void InitStats(BulletSO data, Vector2 dir, string bulletTag)
    {
        _stats = data;
        _sprite.sprite = data.Sprite;
        _dir = dir;
        tag = bulletTag;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(tag))
        {
            var character = other.gameObject.GetComponent<Character>();
            if(character)
                character.TakeDamage(_stats.Damage);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
