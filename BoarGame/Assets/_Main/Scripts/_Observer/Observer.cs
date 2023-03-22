using UnityEngine;

public abstract class Observer : MonoBehaviour
{
    /// <summary>
    /// Receive update from subject
    /// </summary>
    /// <param name="subject"></param>
    public abstract void OnNotify(string message, params object[] args);
}
