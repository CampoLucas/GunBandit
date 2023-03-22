using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WobbleText : MonoBehaviour
{
    private TMP_Text _textMesh;
    private Mesh _mesh;
    private Vector3[] _vertices;

    private List<int> _wordIndexes;
    private List<int> _wordLengths;

    [SerializeField] private bool wobble;
    [SerializeField] private Vector2 amount;
    private void Start()
    {
        _textMesh = GetComponent<TMP_Text>();

        _wordIndexes = new List<int> { 0 };
        _wordLengths = new List<int>();

        var s = _textMesh.text;
        for (var index = s.IndexOf(' '); index > -1; index = s.IndexOf(' ', index + 1))
        {
            _wordLengths.Add(index - _wordIndexes[_wordIndexes.Count - 1]);
            _wordLengths.Add(index + 1);
        }
        _wordLengths.Add(s.Length - _wordIndexes[_wordIndexes.Count - 1]);
    }

    private void Update()
    {
        if (!wobble) return;
        _textMesh.ForceMeshUpdate();
        _mesh = _textMesh.mesh;
        _vertices = _mesh.vertices;

        for (var w = 0; w < _wordIndexes.Count; w++)
        {
            var wordIndex = _wordIndexes[w];
            Vector3 offset = Wobble(Time.time + w);
            
            for (var i = 0; i < _wordLengths[w]; i++)
            {
                var c = _textMesh.textInfo.characterInfo[wordIndex + i];
                var index = c.vertexIndex;
                
                _vertices[index] += (Vector3)offset;
                _vertices[index + 1] += (Vector3)offset;
                _vertices[index + 2] += (Vector3)offset;
                _vertices[index + 3] += (Vector3)offset;
            }
        }
        
        

        _mesh.vertices = _vertices;
        _textMesh.canvasRenderer.SetMesh(_mesh);
    }

    private Vector2 Wobble(float time, float x = 3.3f, float y = 2.5f) => new Vector2(Mathf.Sin(time * x), Mathf.Cos(time * y));
}
