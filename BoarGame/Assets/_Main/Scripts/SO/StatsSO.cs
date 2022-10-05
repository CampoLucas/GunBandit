using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Entities/Stats", order = 1)]
public class StatsSO : ScriptableObject
{
    [Header("Something here")]
    [SerializeField] private string id = "default";
    [SerializeField] private string displayName;
    
    [Header("Render")]
    [SerializeField] private Render render; 
    
    
    public string Id => id;
    public string DisplayName => displayName;
    
    public Sprite Sprite => render.Sprite;
    public Sprite Icon => render.Icon;
    
}

[System.Serializable]
public struct Render
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite icon;

    public Sprite Sprite => sprite;
    public Sprite Icon => icon;
}
