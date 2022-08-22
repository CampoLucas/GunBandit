using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    /// <summary>
    /// Move towards a direction
    /// </summary>
    /// <param name="dir">Direction it moves towards</param>
    public void Move(Vector2 dir)
    {
        var pos = transform.position;
        var moveAmount = speed * Time.deltaTime;
        var move = Vector2.zero;
        if (dir.x > 0) move.x += dir.x + moveAmount;
        if (dir.x < 0) move.x += dir.x + moveAmount;
        if (dir.y > 0) move.y += dir.y + moveAmount;
        if (dir.y < 0) move.y += dir.y + moveAmount;
        float moveMagnitude = Mathf.Sqrt(move.x * move.x + move.y * move.y);
        if (moveMagnitude > moveAmount)
        {
            float ratio = moveAmount / moveMagnitude;
            move *= ratio;
        }
        pos += (Vector3)move;
        transform.position = pos;
    }
    
}
