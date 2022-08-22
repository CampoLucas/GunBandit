using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputHandler _input;
    private Movement _movement;
    private Rotation _rotation;

    private void Awake()
    {
        _input = GetComponent<PlayerInputHandler>();
        _movement = GetComponent<Movement>();
        _rotation = GetComponent<Rotation>();
    }

    private void Update()
    {
        Move(_input.MovementInput);
        Rotate(Input.mousePosition);
    }

    private void Move(Vector2 direction) => _movement.Move(direction);
    private void Rotate(Vector2 mousePos) => _rotation.Rotate(mousePos);
}
