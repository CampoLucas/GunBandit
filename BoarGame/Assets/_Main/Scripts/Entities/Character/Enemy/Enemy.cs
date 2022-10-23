using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private EnemyAI _ai;

    protected override void Awake()
    {
        base.Awake();
        _ai = GetComponent<EnemyAI>();
    }

    private void FixedUpdate()
    {
        // if(!_ai.Target) return;
        // if (Vector3.Distance(transform.position, _ai.transform.position) < 1f)
        // {
        //     Move(_ai.Target.transform.position);
        //     Rotate(_ai.Target.transform.position);
        // }
    }
}
