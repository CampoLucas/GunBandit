using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "Gun", menuName = "Entities/Stats/Weapons/Gun", order = 0)]
public class GunSO : WeaponSO
{
    [SerializeField] private ParticleSystem muzzle;
    
    [Range(0.0001f, 20)] [SerializeField] private float fireRate = 0.1f;
    [Range(0.0001f, 20)][SerializeField] private float reloadSpeed = 1f;

    [Range(0, 500)] [SerializeField] private int ammo = 60;
    [Range(0, 20) ][SerializeField] private int magAmmo = 6;
    
    
    [SerializeField] private int pellets = 5;
    [SerializeField] private float spread = 15f;

    [SerializeField] private bool hasRecoil = true;
    [Range(0, 20)] [SerializeField] private float recoil = 0.5f;
    [Range(0, 20)] [SerializeField] private float snappiness = .2f;
    [Range(0, 20)] [SerializeField] private float returnSpeed = .2f;
    
    [SerializeField] private BulletSO bulletData;
    [SerializeField] private Bullet bulletPrefab;
    
    public ParticleSystem Muzzle => muzzle;
    public float FireRate => fireRate;
    public float ReloadSpeed => reloadSpeed;
    public int Ammo => ammo;
    public int MagAmmo => magAmmo;
    public int Pellets => pellets;
    public float Spread => spread;
    public bool HasRecoil => hasRecoil;
    public float Recoil => recoil;
    public float Snappiness => snappiness;
    public float ReturnSpeed => returnSpeed;
    public BulletSO BulletData => bulletData;
    public Bullet BulletPrefab => bulletPrefab;
}

#if UNITY_EDITOR
[CustomEditor(typeof(GunSO))]
class GunSOEditor : Editor
{
#region SerializedProperties
    private SerializedProperty _id;
    
    private SerializedProperty _sprite;
    private SerializedProperty _muzzle;
    
    private SerializedProperty _displayName;
    private SerializedProperty _icon;
    
    private SerializedProperty _mass;
    private SerializedProperty _linearDrag;

    private SerializedProperty _animation;
    private SerializedProperty _hold;

    private SerializedProperty _throwStrength;
    private SerializedProperty _fireRate;
    private SerializedProperty _reloadSpeed;
    
    private SerializedProperty _ammo;
    private SerializedProperty _magAmmo;

    private SerializedProperty _pellets;
    private SerializedProperty _spread;
    
    private SerializedProperty _hasRecoil;
    private SerializedProperty _recoil;
    private SerializedProperty _snappiness;
    private SerializedProperty _returnSpeed;
    
    private SerializedProperty _bulletData;
    private SerializedProperty _bulletPrefab;

    private bool _identification = true;
    private bool _renderer = true;
    private bool _ui = true;
    private bool _physics = true;
    private bool _weaponStats = true;
    private bool _bulletStats = true;

    private int _weaponType = 0;
    private string[] _weaponTypes = new string[2] { "Automatic", "Shotgun" };
    
    
#endregion

    private void OnEnable()
    {
        _id = serializedObject.FindProperty("id");
        
        _sprite = serializedObject.FindProperty("sprite");
        _muzzle = serializedObject.FindProperty("muzzle");
        
        _displayName = serializedObject.FindProperty("displayName");
        _icon = serializedObject.FindProperty("icon");
        
        _mass = serializedObject.FindProperty("mass");
        _linearDrag = serializedObject.FindProperty("linearDrag");

        _animation = serializedObject.FindProperty("animation");
        _hold = serializedObject.FindProperty("hold");
        
        _throwStrength = serializedObject.FindProperty("throwStrength");
        _fireRate = serializedObject.FindProperty("fireRate");
        _reloadSpeed = serializedObject.FindProperty("reloadSpeed");
        
        _ammo = serializedObject.FindProperty("ammo");
        _magAmmo = serializedObject.FindProperty("magAmmo");

        _pellets = serializedObject.FindProperty("pellets");
        _spread = serializedObject.FindProperty("spread");
        
        _hasRecoil = serializedObject.FindProperty("hasRecoil");
        _recoil = serializedObject.FindProperty("recoil");
        _snappiness = serializedObject.FindProperty("snappiness");
        _returnSpeed = serializedObject.FindProperty("returnSpeed");
        
        _bulletData = serializedObject.FindProperty("bulletData");
        _bulletPrefab = serializedObject.FindProperty("bulletPrefab");
    }

    public override void OnInspectorGUI()
    {
        var gun = target as GunSO;
        serializedObject.Update();

        // ID ---------------------------------------------------------------------------------------------------------
        _identification = EditorGUILayout.BeginFoldoutHeaderGroup(_identification, "Identification");
        if (_identification)
        {
            EditorGUILayout.PropertyField(_id);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        // Sprite, material, colors, additional sprites, etc ----------------------------------------------------------
        _renderer = EditorGUILayout.BeginFoldoutHeaderGroup(_renderer, "Renderer");
        if (_renderer)
        {
            EditorGUILayout.PropertyField(_sprite);
            EditorGUILayout.PropertyField(_muzzle);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        // What is displayed in the ui --------------------------------------------------------------------------------
        _ui = EditorGUILayout.BeginFoldoutHeaderGroup(_ui, "UI");
        if (_ui)
        {
            EditorGUILayout.PropertyField(_displayName);
            EditorGUILayout.PropertyField(_icon);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // Rigidbody --------------------------------------------------------------------------------------------------
        _physics = EditorGUILayout.BeginFoldoutHeaderGroup(_physics, "Physics");
        if (_physics)
        {
            EditorGUILayout.LabelField("RigidBody");
            
            EditorGUILayout.PropertyField(_mass);
            EditorGUILayout.PropertyField(_linearDrag);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        // Animation --------------------------------------------------------------------------------------------------
        EditorGUILayout.LabelField("Animation");
        EditorGUILayout.PropertyField(_animation);
        EditorGUILayout.PropertyField(_hold);

        // Weapon staff -----------------------------------------------------------------------------------------------
        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("Weapon");
        _weaponType = EditorGUILayout.Popup("Type", _weaponType, _weaponTypes);

        // Stats ------------------------------------------------------------------------------------------------------
        _weaponStats = EditorGUILayout.BeginFoldoutHeaderGroup(_weaponStats, "Stats");
        if (_weaponStats)
        {
            EditorGUILayout.PropertyField(_throwStrength);
            if (gun && gun.Hold)
            {
                EditorGUILayout.PropertyField(_fireRate);
            }
            EditorGUILayout.PropertyField(_reloadSpeed);
            
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Ammo");
            EditorGUILayout.PropertyField(_ammo);
            EditorGUILayout.PropertyField(_magAmmo);

            // if it is type shotgun
            if (_weaponType == 1)
            {
                EditorGUILayout.Space(10);
                EditorGUILayout.LabelField("Pellets");
                EditorGUILayout.PropertyField(_pellets);
                EditorGUILayout.PropertyField(_spread);
            }
            
            EditorGUILayout.Space(10);
            EditorGUILayout.LabelField("Recoil");
            EditorGUILayout.PropertyField(_hasRecoil);
            if (gun && gun.HasRecoil)
            {
                EditorGUILayout.PropertyField(_recoil);
                EditorGUILayout.PropertyField(_snappiness);
                EditorGUILayout.PropertyField(_returnSpeed);
            }
        }
        EditorGUILayout.EndFoldoutHeaderGroup();

        // Bullets ----------------------------------------------------------------------------------------------------
        _bulletStats = EditorGUILayout.BeginFoldoutHeaderGroup(_bulletStats, "Bullet Components");
        if (_bulletStats)
        {
            EditorGUILayout.PropertyField(_bulletPrefab);
            EditorGUILayout.PropertyField(_bulletData);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        
        serializedObject.ApplyModifiedProperties();
    }
}
#endif