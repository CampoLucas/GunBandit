using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : Subject
{
    [SerializeField] private List<Observer> subscribers;
    public override List<Observer> Subscribers => subscribers;

    private void Start()
    {
        if(LevelManager.Instance) Subscribe(LevelManager.Instance);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        if (LevelManager.Instance != null)
        {
            NotifyAll("MAIN OBJECT COMPLETED");
            gameObject.SetActive(false);
        }
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
