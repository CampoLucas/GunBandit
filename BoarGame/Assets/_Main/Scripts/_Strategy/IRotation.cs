using UnityEngine;

/// <summary>
/// Interface for rotation
/// </summary>
public interface IRotation
{
    /// <summary>
    /// Rotates towards a position
    /// </summary>
    /// <param name="pos"></param>
    void Rotate(Vector2 pos);
}
