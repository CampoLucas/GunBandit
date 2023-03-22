using System.Collections.Generic;
public interface ISubject
{
    /// <summary>
    /// List of Observers
    /// </summary>
    List<IObserver> Subscribers { get; }
    /// <summary>
    /// Attach an observer to the subject.
    /// </summary>
    /// <param name="observer"></param>
    void Subscribe(IObserver observer);
    /// <summary>
    /// Detach an observer from the subject.
    /// </summary>
    /// <param name="observer"></param>
    void Unsubscribe(IObserver observer);
    /// <summary>
    /// Notify all observers about an event.
    /// </summary>
    void NotifyAll(string message, params object[] args);
}
