using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTowards : Movement
{
    public override void Move(Vector2 dir)
    {
        var direction = (Vector3)dir - transform.position;
        Rigidbody.AddRelativeForce(transform.up * (Stats.Speed * Time.deltaTime), ForceMode2D.Force);
    }
}
