using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Entity
{
    private void Update()
    {
        Move(transform.up);
    }
}
