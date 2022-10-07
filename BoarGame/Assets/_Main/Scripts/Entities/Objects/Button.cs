using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour, ISubject
{
    [SerializeField] private List<IObserver> _observers = new();
    
    public bool Pressed { get; private set; }

    public void Attach(IObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IObserver observer)
    {
        _observers.Remove(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
        {
            observer.OnNotify(this);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!Pressed)
        {
            Pressed = true;
            Notify();
        }
    }
}
