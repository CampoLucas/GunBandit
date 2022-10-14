using UnityEditor;

[CustomEditor(typeof(GunSO))]
public class GunSOEditor : WeaponSOEditor
{
    #region SerializedProperties
    private SerializedProperty _muzzle;
    
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
    #endregion
    
    private bool _bulletStats = true;

    private int _weaponType = 0;
    private readonly string[] _weaponTypes = new string[2] { "Automatic", "Shotgun" };

    protected override void OnEnable()
    {
        base.OnEnable();
        _muzzle = serializedObject.FindProperty("muzzle");
        
        
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
        serializedObject.Update();
        IDLayout();
        RenderLayout();
        UILayout();
        PhysicsLayout();
        AnimationLayout();
        StatsLayout();
        BulletLayout();
        serializedObject.ApplyModifiedProperties();
    }

    protected override void IDLayout(float space = 10)
    {
        EditorGUILayout.Space(space);
        Identification = EditorGUILayout.BeginFoldoutHeaderGroup(Identification, "Identification");
        if (Identification)
        {
            EditorGUILayout.PropertyField(Id);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    protected override void RenderLayout(float space = 10)
    {
        EditorGUILayout.Space(space);
        Renderer = EditorGUILayout.BeginFoldoutHeaderGroup(Renderer, "Renderer");
        if (Renderer)
        {
            EditorGUILayout.PropertyField(Sprite);
            EditorGUILayout.PropertyField(_muzzle);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    protected override void UILayout(float space = 10)
    {
        EditorGUILayout.Space(space);
        UI = EditorGUILayout.BeginFoldoutHeaderGroup(UI, "UI");
        if (UI)
        {
            EditorGUILayout.PropertyField(DisplayName);
            EditorGUILayout.PropertyField(Icon);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    protected override void PhysicsLayout(float space = 10)
    {
        EditorGUILayout.Space(space);
        Physics = EditorGUILayout.BeginFoldoutHeaderGroup(Physics, "Physics");
        if (Physics)
        {
            EditorGUILayout.LabelField("RigidBody");
            EditorGUILayout.PropertyField(PhysicsMaterial);
            EditorGUILayout.PropertyField(Mass);
            EditorGUILayout.PropertyField(Drag);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
    
    protected override void AnimationLayout(float space = 10)
    {
        EditorGUILayout.Space(space);
        EditorGUILayout.LabelField("Animation");
        EditorGUILayout.PropertyField(Animation);
    }
    
    protected override void StatsLayout(float space = 10)
    {
        var gun = target as GunSO;
        EditorGUILayout.Space(space);
        EditorGUILayout.LabelField("Weapon");
        _weaponType = EditorGUILayout.Popup("Type", _weaponType, _weaponTypes);
        
        EditorGUILayout.Space(space);
        WeaponStats = EditorGUILayout.BeginFoldoutHeaderGroup(WeaponStats, "Stats");
        if (WeaponStats)
        {
            EditorGUILayout.PropertyField(ThrowStrength);
            EditorGUILayout.PropertyField(_fireRate);
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
    }
    
    private void BulletLayout(float space = 10)
    {
        EditorGUILayout.Space(space);
        _bulletStats = EditorGUILayout.BeginFoldoutHeaderGroup(_bulletStats, "Bullet Components");
        if (_bulletStats)
        {
            EditorGUILayout.PropertyField(_bulletPrefab);
            EditorGUILayout.PropertyField(_bulletData);
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
    }


}
