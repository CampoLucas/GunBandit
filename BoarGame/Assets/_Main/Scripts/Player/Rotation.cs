using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    private void Update()
    {
        
    }

    public void Rotate(Vector2 mousePos)
    {
        var mousePosition = mousePos;
        if (Camera.main != null) mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        var position = transform.position;
        var direction = new Vector2(mousePosition.x - position.x, mousePosition.y - position.y);
        transform.up = direction;
    }
}
