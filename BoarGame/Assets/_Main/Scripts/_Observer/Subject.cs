using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    /// <summary>
    /// List of Observers
    /// </summary>
    public abstract List<Observer> Subscribers { get; }
    /// <summary>
    /// Attach an observer to the subject.
    /// </summary>
    /// <param name="observer"></param>
    public abstract void Subscribe(Observer observer);
    /// <summary>
    /// Detach an observer from the subject.
    /// </summary>
    /// <param name="observer"></param>
    public abstract void Unsubscribe(Observer observer);
    /// <summary>
    /// Notify all observers about an event.
    /// </summary>
    public abstract void NotifyAll(string message, params object[] args);
}
