using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only the movement
/// </summary>
public class PlayerMovement : MonoBehaviour, IMovement
{
    private CharacterSO _stats;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _stats = GetComponent<Entity>().GetData() as CharacterSO;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Move towards a direction
    /// </summary>
    /// <param name="dir">Direction it moves towards</param>
    public void Move(Vector2 dir)
    {
        _rigidbody.velocity = dir * (_stats.Speed * Time.fixedDeltaTime);
    }

}
