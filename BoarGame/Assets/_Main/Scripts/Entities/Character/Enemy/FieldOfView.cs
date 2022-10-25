using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class FieldOfView : MonoBehaviour
{
    [SerializeField] private float radius = 10;
    [Range(0, 360)] [SerializeField] private float angle = 90;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private Color color = Color.green;
    public GameObject PlayerRef { get; private set; }
    public bool CanSeePlayer;
    public float Radius => radius;
    public float Angle => angle;
    public Color Color => color;

    private void Start()
    {
        //PlayerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVRoutine());
    }
    
    private IEnumerator FOVRoutine()
    {
        var wait = new WaitForSeconds(0.2f);

        while (isActiveAndEnabled)
        {
            yield return wait;
            FieldOfViewCheck();
            Debug.Log("ss");
        }
    }

    private void FieldOfViewCheck()
    {
        var rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);

        if (rangeCheck.Length != 0)
        {
            Debug.Log("print");
            foreach (var player in rangeCheck)
            {
                var target = player.transform;
                var dirToTarget = (target.position - transform.position).normalized;
                if (!PlayerRef)
                    PlayerRef = player.gameObject;

                if (Vector3.Angle(transform.up, dirToTarget) < angle / 2)
                {
                    Debug.Log("print2");
                    var position = transform.position;
                    var distanceToTarget = Vector3.Distance(position, target.position);
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    private void OnSceneGUI()
    {
        var fov = target as FieldOfView;
        if (!fov) return;
        Handles.color = fov.Color;
        var transform = fov.transform;
        var position = transform.position;
        Handles.DrawWireArc(position, Vector3.forward, transform.up, fov.Angle / 2, fov.Radius);
        Handles.DrawWireArc(position, Vector3.forward, fov.transform.up, -fov.Angle / 2, fov.Radius);

        var eulerAngles = fov.transform.eulerAngles;
        var viewAngle01 = DirectionFromAngle(-eulerAngles.z, -fov.Angle / 2);
        var viewAngle02 = DirectionFromAngle(-eulerAngles.z, fov.Angle / 2);

        Handles.DrawLine(position, position + viewAngle01 * fov.Radius);
        Handles.DrawLine(position, position + viewAngle02 * fov.Radius);
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), 0);
    }
}
#endif

