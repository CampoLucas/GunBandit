using UnityEngine;
using System;

public class Grid<T>
{
    private int _width;
    private int _height;
    private float _cellSize;
    private Vector3 _originPosition;
    private T[,] _gridArray;

#if UNITY_EDITOR
    private TextMesh[,] _debugText;
#endif

    public delegate void GridChangedHandler(int x, int y);
    public event GridChangedHandler OnGridChanged;

    public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<Grid<T>, int, int, T> createObj)
    {
        _width = width;
        _height = height;
        _cellSize = cellSize;
        _originPosition = originPosition;
        _gridArray = new T[width, height];

        for (var x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (var y = 0; y < _gridArray.GetLength(1); y++)
            {
                var pos = GetWorldPosition(x, y);
                _gridArray[x, y] = createObj(this, (int)pos.x, (int)pos.y);
                //_gridArray[x, y] = createObj(this, x, y);
            }
        }
#if UNITY_EDITOR
        Debug.Log("Grid size: (" + width + ", " + height + ")");
        
        _debugText = new TextMesh[width, height];
        
        for (var x = 0; x < _gridArray.GetLength(0); x++)
        {
            for (var y = 0; y < _gridArray.GetLength(1); y++)
            {
                _debugText[x, y] = CreateWorldText(_gridArray[x, y]?.ToString(), null,
                    GetWorldPosition(x, y) + new Vector3(cellSize, cellSize) * .5f, 20, Color.white,
                    TextAnchor.MiddleCenter, TextAlignment.Center, 50);
                
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

        OnGridChanged += UpdateDebug;
#endif
    }

    public int GetWidth()
    {
        return _width;
    }

    public int GetHeight()
    {
        return _height;
    }

    public float GetCellSize()
    {
        return _cellSize;
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

    public void SetObject(int x, int y, T value)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            _gridArray[x, y] = value;
            OnGridChanged?.Invoke(x, y);
        }
    }

    public void SetObject(Vector3 worldPos, T value)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        SetObject(x, y, value);
    }

    public T GetObject(int x, int y)
    {
        if (x >= 0 && y >= 0 && x < _width && y < _height)
        {
            return _gridArray[x, y];
        }
        else
        {
            return default;
        }
    }

    public T GetObject(Vector3 worldPos)
    {
        int x, y;
        GetXY(worldPos, out x, out y);
        return GetObject(x, y);
    }


#if UNITY_EDITOR

    private void UpdateDebug(int x, int y)
    {
        _debugText[x, y].text = _gridArray[x, y].ToString();
    }
    private TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition,
        int fontSize, Color color, TextAnchor textAnchor, TextAlignment textAlignment, int sortingOrder)
    {
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
