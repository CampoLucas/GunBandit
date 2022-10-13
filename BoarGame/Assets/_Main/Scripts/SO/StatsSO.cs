using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Stats", menuName = "Entities/Stats", order = 1)]
public class StatsSO : ScriptableObject
{
    [SerializeField] private string id = "default";
    [SerializeField] private Sprite sprite;
    
    
    public string Id => id;
    public Sprite Sprite => sprite;
    
}



