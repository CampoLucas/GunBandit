using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;


public class Enemy : Character
{
    private EnemyAI _ai;
    private AIDestinationSetter _destination;
    private AIPath _path;
    private FollowRoute _route;
    [SerializeField] private FieldOfView shortView;
    [SerializeField] private FieldOfView longView;

    protected override void Awake()
    {
        base.Awake();
        _ai = GetComponent<EnemyAI>();
        _destination = GetComponent<AIDestinationSetter>();
        _path = GetComponent<AIPath>();
        _route = GetComponent<FollowRoute>();
    }

    private void Start()
    {
        _ai.OnMovementSpeedChanged += SetMaxMovementSpeed;
        _ai.OnCanRotate += SetCanRotate;
        _ai.OnRotateTowardsPlayer += Rotate;
        _ai.OnFire += Fire;
        _ai.OnChangeTarget += SetFollowTarget;
        _route.OnPointChanged += SetFollowTarget;
    }

    private void OnDisable()
    {
        _ai.OnMovementSpeedChanged -= SetMaxMovementSpeed;
        _ai.OnCanRotate -= SetCanRotate;
        _ai.OnRotateTowardsPlayer -= Rotate;
        _ai.OnFire -= Fire;
        _ai.OnChangeTarget -= SetFollowTarget;
        _route.OnPointChanged -= SetFollowTarget;
    }
    public void SetFollowTarget(Transform target) => _destination.target = target;
    public void SetMaxMovementSpeed(float speed) => _path.maxSpeed = speed;
    public void SetCanRotate(bool canRotate) => _path.enableRotation = canRotate;
    public void ChangePoint() => _route.ChangePoint();
    public bool CanSeePlayer() => shortView.CanSeePlayer;
    public bool IsAlerted() => longView.CanSeePlayer;
    public GameObject GetPlayerRef() => longView.PlayerRef;
    public Transform GetSpawnPos() => _route.SpawnPoint;
    public Transform GetCurrentPoint() => _route.CurrentPoint;
}
