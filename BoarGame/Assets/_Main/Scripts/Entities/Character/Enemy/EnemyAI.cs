using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyAI : MonoBehaviour
{
    private Enemy _enemy;
    private EnemySO _stats;
    private float _lastSeenTime;
    private bool _playerDetected;
    private bool _follow;
    
    [Header("Movement components")]
    
    
    public Action<Transform> OnChangeTarget;
    public Action<float> OnMovementSpeedChanged;
    public Action<bool> OnCanRotate;
    public Action OnFire;
    public Action<Vector2> OnRotateTowardsPlayer;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _stats = _enemy.GetData() as EnemySO;
    }

    private void Start()
    {
        transform.position = _enemy.GetSpawnPos().position;
        OnMovementSpeedChanged?.Invoke(_stats.IdleSpeed);
    }

    

    private void Update()
    {
        

        if (_enemy.CanSeePlayer())
        {
            _follow = true;
        }
        else if(_playerDetected && !_enemy.CanSeePlayer())
        {
            _follow = false;
        }
        else if (!(_enemy.CanSeePlayer() || _enemy.IsAlerted()) && Vector3.Distance(transform.position, _enemy.GetCurrentPoint().position) < 0.1f && !_playerDetected)
        {
            ChangePoint();
        }
        if(_follow) FollowPlayer();
        else ReturnToPoint();
    }

    private void FollowPlayer()
    {
        if(!_enemy.GetPlayerRef()) return;
        _lastSeenTime = Time.time;
        var pos = transform.position;
        var playerTransform = _enemy.GetPlayerRef().transform;
        var playerPos = playerTransform.position;
        var dirToTarget = (playerPos - pos).normalized;
        var distanceToPlayer = Vector3.Distance(pos, playerPos);
        if (!_playerDetected)
        {
            OnChangeTarget?.Invoke(playerTransform);
            _playerDetected = true;
// #if UNITY_EDITOR
//             Debug.Log("Player detected");
// #endif
        }
        _lastSeenTime = Time.time;
        OnCanRotate?.Invoke(false);
        OnRotateTowardsPlayer?.Invoke(playerPos);
        if (distanceToPlayer < _stats.FollowDistance && distanceToPlayer > _stats.CloseDistance)
        {
            OnFire?.Invoke();
            OnMovementSpeedChanged?.Invoke(_stats.FollowSpeed);
                
        }
        else if (distanceToPlayer < _stats.CloseDistance)
        {
            OnFire?.Invoke();
            OnMovementSpeedChanged?.Invoke(_stats.CloseSpeed);
        }
    }

    private void ReturnToPoint()
    {
        OnCanRotate?.Invoke(true);
        OnMovementSpeedChanged?.Invoke(_stats.IdleSpeed);
        if (!(_lastSeenTime + _stats.LoseDelay < Time.time)) return;
        _lastSeenTime = Time.time;
        _playerDetected = false;
// #if UNITY_EDITOR
//         Debug.Log("Player lost");
// #endif
        OnChangeTarget?.Invoke(_enemy.GetCurrentPoint());
        OnCanRotate?.Invoke(true);
    }

    private void ChangePoint()
    {
        _enemy.ChangePoint();
// #if UNITY_EDITOR
//         Debug.Log("Point reached");
// #endif
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("coll: " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Bullet"))
        {
            _follow = true;
            Debug.Log("ouch");
        }
    }
}

