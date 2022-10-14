using System;
using UnityEditor;

[CustomEditor(typeof(GrenadeSO))]
public class GrenadeSOEditor : WeaponSOEditor
{
    #region SerializedProperties
    

    
    #endregion

    protected override void OnEnable()
    {
        base.OnEnable();
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
        var grenade = target as GrenadeSO;
        EditorGUILayout.Space(space);
        EditorGUILayout.LabelField("Grenade");
        // _weaponType = EditorGUILayout.Popup("Type", _weaponType, _weaponTypes);
        
        EditorGUILayout.Space(space);
        // _weaponStats = EditorGUILayout.BeginFoldoutHeaderGroup(_weaponStats, "Stats");
        // if (_weaponStats)
        // {
        //     EditorGUILayout.PropertyField(_throwStrength);
        //     EditorGUILayout.PropertyField(_fireRate);
        //     EditorGUILayout.PropertyField(_reloadSpeed);
        //     
        //     EditorGUILayout.Space(10);
        //     EditorGUILayout.LabelField("Ammo");
        //     EditorGUILayout.PropertyField(_ammo);
        //     EditorGUILayout.PropertyField(_magAmmo);
        //
        //     // if it is type shotgun
        //     if (_weaponType == 1)
        //     {
        //         EditorGUILayout.Space(10);
        //         EditorGUILayout.LabelField("Pellets");
        //         EditorGUILayout.PropertyField(_pellets);
        //         EditorGUILayout.PropertyField(_spread);
        //     }
        //     
        //     EditorGUILayout.Space(10);
        //     EditorGUILayout.LabelField("Recoil");
        //     EditorGUILayout.PropertyField(_hasRecoil);
        //     if (gun && gun.HasRecoil)
        //     {
        //         EditorGUILayout.PropertyField(_recoil);
        //         EditorGUILayout.PropertyField(_snappiness);
        //         EditorGUILayout.PropertyField(_returnSpeed);
        //     }
        // }
        // EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
