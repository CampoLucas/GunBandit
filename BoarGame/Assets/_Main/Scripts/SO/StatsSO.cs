using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stats", menuName = "Entities/Stats", order = 1)]
public class StatsSO : ScriptableObject
{
    [SerializeField] private string id = "default";
    [SerializeField] private Sprite sprite;
    [SerializeField] private float speed;
    public string Id => id;
    public Sprite Sprite => sprite;
    public float Speed => speed;
}
