using System;
using UnityEditor;

[CustomEditor(typeof(StatsSOEditor))]
public class StatsSOEditor : Editor
{
    #region SerializedProperties
    protected SerializedProperty Id;
    protected SerializedProperty Sprite;
    #endregion
    
    protected bool Identification = true;
    protected bool Renderer = true;

    protected virtual void OnEnable()
    {
        Id = serializedObject.FindProperty("id");
        Sprite = serializedObject.FindProperty("sprite");
    }

    protected virtual void IDLayout(float space = 10) { }
    
    protected virtual void RenderLayout(float space = 10) { }
}
