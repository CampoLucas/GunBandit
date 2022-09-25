using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IProduct<StatsSO>
{
    [SerializeField] private StatsSO stats;
    //private IMovement _movement;
    
    public StatsSO GetData() => stats;
    //public IMovement Movement => _movement;

    protected virtual void Awake()
    {
        //_movement = GetComponent<IMovement>();
    }

    //protected void Move(Vector2 direction) => Movement.Move(direction);
}
