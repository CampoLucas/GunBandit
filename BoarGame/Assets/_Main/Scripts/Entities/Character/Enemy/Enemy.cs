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
    private FollowLinearRoute _linearRoute;
    [SerializeField] private FieldOfView shortView;
    [SerializeField] private FieldOfView longView;

    protected override void Awake()
    {
        base.Awake();
        _ai = GetComponent<EnemyAI>();
        _destination = GetComponent<AIDestinationSetter>();
        _path = GetComponent<AIPath>();
        _linearRoute = GetComponent<FollowLinearRoute>();
    }

    private void Start()
    {
        _ai.OnSpeedChanged += SetMaxSpeed;
        _ai.OnFire += Fire;
        _ai.OnChangeTarget += SetFollowTarget;
        _linearRoute.OnPointChanged += SetFollowTarget;
    }

    private void OnDisable()
    {
        _ai.OnSpeedChanged -= SetMaxSpeed;
        _ai.OnFire -= Fire;
        _ai.OnChangeTarget -= SetFollowTarget;
        _linearRoute.OnPointChanged -= SetFollowTarget;
    }

    public void SetFollowTarget(Transform target) => _destination.target = target;
    public void SetMaxSpeed(float speed) => _path.maxSpeed = speed;
    public void ChangePoint() => _linearRoute.ChangePoint();
    public bool CanSeePlayer() => shortView.CanSeePlayer;
    public bool IsAlerted() => longView.CanSeePlayer;
    public GameObject GetPlayerRef() => longView.PlayerRef;
    public Transform GetSpawnPos() => _linearRoute.SpawnPoint;
    public Transform GetCurrentPoint() => _linearRoute.CurrentPoint;
}
