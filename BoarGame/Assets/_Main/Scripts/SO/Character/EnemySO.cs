using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Entities/Stats/Characters/Enemy", order = 1)]
public class EnemySO : CharacterSO
{
    [Header("Movement")]
    [SerializeField] private float idleSpeed = 3;
    [SerializeField] private float followSpeed = 3;
    [SerializeField] private float closeSpeed = 0;
    [SerializeField] private float followDistance = 15;
    [SerializeField] private float closeDistance = 8;
    [SerializeField] private float loseDelay = 15;

    [SerializeField] private Weapon2 weapon;

    public float IdleSpeed => idleSpeed;
    public float FollowSpeed => followSpeed;
    public float CloseSpeed => closeSpeed;
    public float FollowDistance => followDistance;
    public float CloseDistance => closeDistance;
    public float LoseDelay => loseDelay;
}
