using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : Subject
{
    private PlayerInputHandler _inputs;
    [SerializeField] private List<Observer> subscribers;
    public override List<Observer> Subscribers => subscribers;

    private void Start()
    {
        var subscriber = LevelManager.Instance;
        if(subscriber) Subscribe(subscriber);
    }
    private void Escape()
    {
        NotifyAll("SCAPE");
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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        if(_inputs == null)
            _inputs = other.GetComponent<PlayerInputHandler>();
        _inputs.OnInteractPerformed += Escape;
        
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if(!other.CompareTag("Player")) return;
        _inputs.OnInteractPerformed -= Escape;
    }
}
