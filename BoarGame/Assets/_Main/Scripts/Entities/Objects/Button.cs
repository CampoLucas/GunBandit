using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    private bool _isPressed;
    private bool _prevPressedState;
    
    public UnityEvent onPressed;
    public UnityEvent onReleased;
    
    private void Press()
    {
        _isPressed = true;
        if (!_isPressed && _prevPressedState == _isPressed) return;
        _prevPressedState = _isPressed;
        onPressed?.Invoke();
    }
    
    private void Release()
    {
        _isPressed = false;
        if (_isPressed && _prevPressedState == _isPressed) return;
        _prevPressedState = _isPressed;
        onReleased?.Invoke();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Press();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Release();
    }
}
