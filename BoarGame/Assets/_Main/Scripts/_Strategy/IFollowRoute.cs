using System;
using System.Collections.Generic;
using UnityEngine;

public interface IFollowRoute
{
    Transform SpawnPoint { get; }
    Transform CurrentPoint { get; }
    Action<Transform> OnPointChanged { get; }
    void ChangePoint();
}
