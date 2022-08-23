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

    private void Awake()
    {
        _camera = Camera.main;
    }

    /// <summary>
    /// Rotates to a position
    /// </summary>
    /// <param name="pos"></param>
    public void Rotate(Vector2 pos)
    {
        var mousePosition = pos;
        mousePosition = _camera.ScreenToWorldPoint(mousePosition);
        var transform1 = transform;
        var position = transform1.position;
        var direction = new Vector2(mousePosition.x - position.x, mousePosition.y - position.y);
        transform1.up = direction;
    }
}
