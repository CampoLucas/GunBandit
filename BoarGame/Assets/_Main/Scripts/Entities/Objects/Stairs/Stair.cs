using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : MonoBehaviour
{
    private Player _player;
    [SerializeField] private string floor;
    [SerializeField] private Stair nextStairs;

    private void UseStairs()
    {
        _player.transform.position = nextStairs.transform.position;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        if (_player == null)
            _player = other.GetComponent<Player>();
        var inputs = other.GetComponent<PlayerInputHandler>();
        if (!inputs) return;
        inputs.OnInteractStarted += UseStairs;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        
        var inputs = other.GetComponent<PlayerInputHandler>();
        if (!inputs) return;
        inputs.OnInteractStarted -= UseStairs;
    }
}
