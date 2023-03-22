using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmageTest : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var player = other.GetComponent<Player>();
        player.TakeDamage(100);
    }
}
