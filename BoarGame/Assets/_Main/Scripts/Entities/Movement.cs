using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour, IMovement
{
    private StatsSO _stats;

    private void Awake()
    {
        _stats = GetComponent<Entity>().Data;
    }

    public void Move(Vector2 dir)
    {
        transform.position += (Vector3)dir * (_stats.Speed *Time.deltaTime);
    }
}