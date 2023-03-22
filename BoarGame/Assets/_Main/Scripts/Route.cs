using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [SerializeField] private List<Transform> points;
    public List<Transform> Points => points;

    private void Awake()
    {
        var transforms = GetComponentsInChildren<Transform>();
        foreach (var transform in transforms)
        {
            if(transform == this.transform) continue;
            points.Add(transform);
        }
    }
}
