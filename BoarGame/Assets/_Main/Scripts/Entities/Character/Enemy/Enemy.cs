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
    private FieldOfView _view;

    protected override void Awake()
    {
        base.Awake();
        _ai = GetComponent<EnemyAI>();
        _destination = GetComponent<AIDestinationSetter>();
        _path = GetComponent<AIPath>();
        _view = GetComponent<FieldOfView>();
    }

    private void Start()
    {
        _ai.OnSpeedChanged += SetMaxSpeed;
        _ai.OnFire += Fire;
        _ai.OnChangeTarget += SetTargetTransform;
    }

    private void OnDisable()
    {
        _ai.OnSpeedChanged -= SetMaxSpeed;
        _ai.OnFire -= Fire;
        _ai.OnChangeTarget -= SetTargetTransform;
    }

    public void SetTargetTransform(Transform target)
    {
        _destination.target = target;
    }

    public void SetMaxSpeed(float speed)
    {
        _path.maxSpeed = speed;
    }

    public bool CanSeePlayer()
    {
        return _view.CanSeePlayer;
    }

    public GameObject GetPlayerRef()
    {
        return _view.PlayerRef;
    }
}
