using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Window : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Collider2D _collider;
    private Light2D _light;

    private void Awake()
    {
        _sprite = GetComponentInChildren<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _light = GetComponentInChildren<Light2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.gameObject.layer != LayerMask.NameToLayer("Bullets")) return;
        Debug.Log("Break");
        _sprite.color = new Color(.5f, .5f, .5f, .5f);
        _light.enabled = false;
        _collider.enabled = false;
    }
}
