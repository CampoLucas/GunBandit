
public interface IObserver
{
    /// <summary>
    /// Receive update from subject
    /// </summary>
    /// <param name="subject"></param>
    void OnNotify(ISubject subject);
}
