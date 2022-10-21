using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private int[,] _gridArray;

#if UNITY_EDITOR
    private TextMesh[,] _debugText;
#endif

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _gridArray = new int[width, height];
        
        
#if UNITY_EDITOR
        _debugText = new TextMesh[width, height];
        Debug.Log("Grid size: (" + width + ", " + height + ")");
#endif

        for (var x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (var y = 0; y < _gridArray.GetLength(1); y++)
            {
#if UNITY_EDITOR
                _debugText[x, y] = CreateWorldText(_gridArray[x, y].ToString(), null,
                    GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white,
                    TextAnchor.MiddleCenter, TextAlignment.Center, 50);
                
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
#endif
            }
        }
#if UNITY_EDITOR
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);
#endif
        
        SetValue(0,0, 56);
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize + _originPosition;
    }
    
    private void GetXY(Vector3 worldPos, out int x, out int y)
    {
        var pos = (worldPos - _originPosition);
        x = Mathf.FloorToInt(pos.x / _cellSize);
        y = Mathf.FloorToInt(pos.y / _cellSize);
    }

    public void SetValue(int x, int y, int value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = value;
#if UNITY_EDITOR
            _debugText[x, y].text = _gridArray[x, y].ToString();
#endif
        }
    }

    public void SetValue(Vector3 worldPos, int value)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        SetValue(x, y, value);
    }

    public int GetValue(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return 0;
        }
    }

    public int GetValue(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetValue(x, y);
    }


#if UNITY_EDITOR
    private TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition,
        int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        if(color == null) color = Color.white;
        return CreateWorldText(parent, text, localPosition, fontSize, color, textAnchor, textAlignment, sortingOrder);
    }
    private TextMesh CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color,
        TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
        var gameObject = new GameObject("World_Text", typeof(TextMesh));
        var transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        var textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        textMesh.GetComponent<MeshRenderer>().sortingLayerName = "UIText";
        return textMesh;
    }
#endif
}
