using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Character", menuName = "Entities/Stats/Character", order = 1)]
public class CharacterSO : StatsSO
{
    [Header("Movement")]
    [Range(0f, 800f)][SerializeField] private float speed = 2f;
    
    public float Speed => speed;
}

#if UNITY_EDITOR
[CustomEditor(typeof(CharacterSO))]
class CharacterSOEditor : Editor
{
#region SerializedProperties
    private SerializedProperty _id;
    
    private SerializedProperty _sprite;
    
    private SerializedProperty _speed;
    
    private bool _identification = true;
    private bool _renderer = true;
    private bool _stats = true;
#endregion

    private void OnEnable()
    {
        _id = serializedObject.FindProperty("id");
        _sprite = serializedObject.FindProperty("sprite");
        _speed = serializedObject.FindProperty("speed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        
        _identification = EditorGUILayout.BeginFoldoutHeaderGroup(_identification, "Identification");
        if (_identification)
        {
            EditorGUILayout.PropertyField(_id);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        _renderer = EditorGUILayout.BeginFoldoutHeaderGroup(_renderer, "Renderer");
        if (_renderer)
        {
            EditorGUILayout.PropertyField(_sprite);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        serializedObject.ApplyModifiedProperties();
        
        _stats = EditorGUILayout.BeginFoldoutHeaderGroup(_stats, "Stats");
        if (_stats)
        {
            EditorGUILayout.PropertyField(_speed);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
