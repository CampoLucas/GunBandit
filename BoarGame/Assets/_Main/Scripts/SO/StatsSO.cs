using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Entities/Stats", order = 1)]
public class StatsSO : ScriptableObject
{
    [SerializeField] private string id = "default";
    [SerializeField] private string displayName;
    
    [Header("Render")]
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite icon;
    
    [Header("Stats")]
    [Range(0.0001f, 200)][SerializeField] private float speed;
    public string Id => id;
    public string DisplayName => displayName;
    public Sprite Sprite => sprite;
    public Sprite Icon => icon;
    public float Speed => speed;
}
