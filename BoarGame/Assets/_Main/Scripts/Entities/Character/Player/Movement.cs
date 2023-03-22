using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only the movement
/// </summary>
public class Movement : MonoBehaviour, IMovement
{
    protected CharacterSO Stats;
    protected Rigidbody2D Rigidbody;

    private void Awake()
    {
        Stats = GetComponent<Entity>().GetData() as CharacterSO;
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Move towards a direction
    /// </summary>
    /// <param name="dir">Direction it moves towards</param>
    public virtual void Move(Vector2 dir)
    {
        Rigidbody.velocity = dir.normalized * (Stats.Speed * Time.fixedDeltaTime);
    }

}
