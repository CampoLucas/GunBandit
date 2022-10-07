using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Entities/Stats/Character", order = 1)]
public class CharacterSO : StatsSO
{
    [Header("Stats")]
    [Range(2f, 10f)][SerializeField] private float speed;
    
    public float Speed => speed;
}
