using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TittleFont : MonoBehaviour
{
    private TMP_Text _textMesh;
    private Mesh _mesh;
    private Vector3[] _vertices; 
    private void Start()
    {
        _textMesh = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _textMesh.ForceMeshUpdate();
        _mesh = _textMesh.mesh;
        _vertices = _mesh.vertices;

        for (int i = 0; i < _vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);

            _vertices[i] = _vertices[i] + offset;
        }

        _mesh.vertices = _vertices;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    private Vector2 Wobble(float time) => new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f));
}
