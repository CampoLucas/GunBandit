using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid _grid;
    
    private void Start()
    {
        _grid = new Grid(4, 2, 1f, new Vector3(2, 0));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _grid.SetValue(GetMouseWorldPosition(), 47);
        }

        if (Input.GetMouseButtonDown(1))
        {
#if UNITY_EDITOR
            Debug.Log(_grid.GetValue(GetMouseWorldPosition()));
#endif
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        var vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private Vector3 GetMouseWorldPositionWithZ(Vector3 screenPos, Camera worldCamera)
    {
        return worldCamera.ScreenToWorldPoint(screenPos);
    }
}
