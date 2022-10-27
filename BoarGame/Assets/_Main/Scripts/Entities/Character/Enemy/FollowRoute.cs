using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoute : MonoBehaviour
{
    private int _index = 0;
    private int _dir = 1;
    [SerializeField] private RouteType type;
    [SerializeField] private Route route;
    public Transform SpawnPoint => route.Points[0];
    public Transform CurrentPoint => route.Points[_index];

    public Action<Transform> OnPointChanged;

    public void ChangePoint()
    {
        if (type == RouteType.Linear)
        {
            if ((_index >= route.Points.Count - 1 && _dir > 0) || (_index <= 0 && _dir < 0))
                _dir *= -1;
            _index += _dir;
            OnPointChanged?.Invoke(CurrentPoint);
        }
        else
        {
            if (_index >= route.Points.Count - 1)
                _index = 0;
            else
                _index ++;
            OnPointChanged?.Invoke(CurrentPoint);
        }
        
    }
}
public enum RouteType { Linear, Circle}
