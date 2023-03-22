using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    private Grid<GameObject> _grid;
    [SerializeField] private GameObject objPrefab;
    
    private void Start()
    {
        _grid = new Grid<GameObject>(4, 2, 1f, new Vector3(2, 0), (Grid<GameObject> grid, int x, int y) => Create(x, y));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //_grid.SetObject(GetMouseWorldPosition(), true);
        }

        if (Input.GetMouseButtonDown(1))
        {
#if UNITY_EDITOR
            Debug.Log(_grid.GetObject(GetMouseWorldPosition()));
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

    private GameObject Create(int x, int y)
    {
        var obj = Instantiate(objPrefab);
        obj.transform.position = new Vector3(x, y);
        return obj;
    }
}
