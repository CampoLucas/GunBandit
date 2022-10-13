using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// It handles the rotation
/// </summary>
public class Rotation : MonoBehaviour, IRotation
{
    private Camera _camera;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Rotates to a position
    /// </summary>
    /// <param name="pos"></param>
    public void Rotate(Vector2 pos)
    {
        // var mousePosition = pos;
        // mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        // var transform1 = transform;
        // var position = transform1.position;
        // var direction = new Vector2(mousePosition.x - position.x, mousePosition.y - position.y);
        // transform1.up = direction;
        
        var mousePosition = pos;
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);

        var lookDir = mousePosition - _rigidbody.position;
        var angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        _rigidbody.rotation = angle;
    }
}
