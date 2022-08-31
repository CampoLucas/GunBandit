using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Only the movement
/// </summary>
public class PlayerMovement : MonoBehaviour, IMovement
{
    private StatsSO _stats;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _stats = GetComponent<Entity>().Data;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Move towards a direction
    /// </summary>
    /// <param name="dir">Direction it moves towards</param>
    public void Move(Vector2 dir)
    {
        var pos = transform.position;
        var moveamount = _stats.Speed * Time.deltaTime;
        var move = Vector2.zero;
        if (dir.x > 0) move.x += dir.x + moveamount;
        if (dir.x < 0) move.x += dir.x + moveamount;
        if (dir.y > 0) move.y += dir.y + moveamount;
        if (dir.y < 0) move.y += dir.y + moveamount;
        float movemagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (movemagnitude > moveamount)
        {
            float ratio = moveamount / movemagnitude;
            move *= ratio;
        }
        pos += (Vector3)move;
        transform.position = pos;

        //var velocity = _rigidbody.velocity;
        //var moveAmount = _stats.Speed * Time.deltaTime;
        //var move = Vector2.zero;
        //if (dir.x > 0) move.x += dir.x + moveAmount;
        //if (dir.x < 0) move.x += dir.x + moveAmount;
        //if (dir.y > 0) move.y += dir.y + moveAmount;
        //if (dir.y < 0) move.y += dir.y + moveAmount;
        //float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        //if (moveMagnitude > moveAmount)
        //{
        //    float ratio = moveAmount / moveMagnitude;
        //    move *= ratio;
        //}
        //velocity += move;
        //_rigidbody.velocity = velocity;

        //_rigidbody.velocity = dir * _stats.Speed;

    }

}
