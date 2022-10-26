using System;
using System.Collections.Generic;
using UnityEngine;

public class FollowLinearRoute : MonoBehaviour
{
    private int _index = 0;
    private int _dir = 1;
    [SerializeField] private List<Transform> points;
    public Transform SpawnPoint => points[0];
    public Transform CurrentPoint => points[_index];

    public Action<Transform> OnPointChanged;

    public void ChangePoint()
    {
        if ((_index >= points.Count - 1 && _dir > 0) || (_index <= 0 && _dir < 0) )
            _dir *= -1;
        _index += _dir;
        OnPointChanged?.Invoke(CurrentPoint);
    }
}
