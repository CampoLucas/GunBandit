using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Entities/Stats/Character", order = 1)]
public class CharacterSO : StatsSO
{
    [Header("Stats")]
    [Range(0.0001f, 200)][SerializeField] private float speed;
    
    public float Speed => speed;
}
