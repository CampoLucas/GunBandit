using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : Subject
{
    private bool _isPressed;
    private bool _prevPressedState;
    //protected PlayerInputHandler Inputs;

    [SerializeField] private List<Observer> subscribers;
    
    public override List<Observer> Subscribers => subscribers;
    //public UnityEvent onPressed;
    //public UnityEvent onReleased;
    
    protected virtual void Press()
    {
        _isPressed = true;
        if (!_isPressed && _prevPressedState == _isPressed) return;
        _prevPressedState = _isPressed;
        //onPressed?.Invoke();
    }
    
    protected virtual void Release()
    {
        _isPressed = false;
        if (_isPressed && _prevPressedState == _isPressed) return;
        _prevPressedState = _isPressed;
        //onReleased?.Invoke();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        
        var inputs = other.GetComponent<PlayerInputHandler>();
        if (!inputs) return;
        inputs.OnInteractStarted += Press;
        inputs.OnInteractCancelled += Release;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        
        var inputs = other.GetComponent<PlayerInputHandler>();
        if (!inputs) return;
        inputs.OnInteractStarted -= Press;
        inputs.OnInteractCancelled -= Release;
    }


    public override void Subscribe(Observer observer)
    {
        if (subscribers.Contains(observer)) return;
        subscribers.Add(observer);
    }

    public override void Unsubscribe(Observer observer)
    {
        if (subscribers.Contains(observer)) return;
        subscribers.Remove(observer);
    }

    public override void NotifyAll(string message, params object[] args)
    {
        foreach (var t in subscribers)
            t.OnNotify(message, args);
    }
}
