using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stair : Subject
{
    private Player _player;
    [SerializeField] private List<Observer> subscribers;
    [SerializeField] private string floor;
    [SerializeField] private Stair nextStairs;

    public string Floor => floor;

    public override List<Observer> Subscribers => subscribers;
    private void UseStairs()
    {
        _player.transform.position = nextStairs.transform.position;
        NotifyAll(nextStairs.Floor);
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
