using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, IProduct<StatsSO>
{
    [SerializeField] private StatsSO stats;
    public StatsSO GetData() => stats;
    

}
