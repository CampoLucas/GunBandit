using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSO : StatsSO
{
    [SerializeField] private string displayName;
    [SerializeField] private Sprite icon;
    [SerializeField] private PhysicsMaterial2D physicsMaterial;
    [SerializeField] private float mass = 1f;
    [SerializeField] private float drag = .5f;
    //ToDo: animation script
    [SerializeField] private AnimationType animation = AnimationType.Empty;
    [Range(0.0001f, 50)][SerializeField] private float throwStrength = 5f;
    
    
    public float ThrowStrength => throwStrength;

    public PhysicsMaterial2D PhysicsMaterial => physicsMaterial;
    public float Mass => mass;
    public float Drag => drag;
    public AnimationType Animation => animation;
    public string DisplayName => displayName;
    public Sprite Icon => icon;

}

//ToDo: animation script
public enum AnimationType { Empty, Pistol, Shotgun}
