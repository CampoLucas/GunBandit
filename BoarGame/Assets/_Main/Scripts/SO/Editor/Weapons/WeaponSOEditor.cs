using UnityEditor;

[CustomEditor(typeof(WeaponSO))]
public class WeaponSOEditor : StatsSOEditor
{
    #region SerializedProperties
    protected SerializedProperty DisplayName;
    protected SerializedProperty Icon;

    protected SerializedProperty PhysicsMaterial;
    protected SerializedProperty Mass;
    protected SerializedProperty Drag;

    protected SerializedProperty Animation;

    protected SerializedProperty ThrowStrength;
    #endregion
    
    protected bool UI = true;
    protected bool Physics = true;
    protected bool WeaponStats = true;

    protected override void OnEnable()
    {
        base.OnEnable();
        DisplayName = serializedObject.FindProperty("displayName");
        Icon = serializedObject.FindProperty("icon");

        PhysicsMaterial = serializedObject.FindProperty("physicsMaterial");
        Mass = serializedObject.FindProperty("mass");
        Drag = serializedObject.FindProperty("drag");

        Animation = serializedObject.FindProperty("animation");
        
        ThrowStrength = serializedObject.FindProperty("throwStrength");
    }

    protected virtual void UILayout(float space = 10) { }
    protected virtual void PhysicsLayout(float space = 10) { }
    protected virtual void AnimationLayout(float space = 10) { }
    protected virtual void StatsLayout(float space = 10) { }
}
