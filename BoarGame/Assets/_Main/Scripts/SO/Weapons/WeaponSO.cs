using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : StatsSO
{
    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float linearDrag = .5f;
    //ToDo: animation script
    [SerializeField] private AnimationType animation = AnimationType.Empty;
    [Range(0.0001f, 50)][SerializeField] private float throwStrength = 5f;
    
    
    public float ThrowStrength => throwStrength;
    
    public float Mass => mass;
    public float LinearDrag => linearDrag;
    public AnimationType Animation => animation;
    public string DisplayName => displayName;
    public Sprite Icon => icon;

}

//ToDo: animation script
public enum AnimationType { Empty, Pistol, Shotgun}
