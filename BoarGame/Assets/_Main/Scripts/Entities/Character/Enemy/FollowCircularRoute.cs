using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowCircularRoute : MonoBehaviour, IFollowRoute
{
    private int _index = 0;
    [SerializeField] private List<Transform> points;
    public Transform SpawnPoint => points[0];
    public Transform CurrentPoint => points[_index];
    public Action<Transform> OnPointChanged { get; set; }

    public void ChangePoint()
    {
        if (_index >= points.Count - 1)
            _index = 0;
        _index ++;
        OnPointChanged?.Invoke(CurrentPoint);
    }
}
