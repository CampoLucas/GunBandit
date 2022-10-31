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
                Debug.Log("Player detected");
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
        else if(_playerDetected && !_enemy.CanSeePlayer())
        {
            if(!_enemy.GetPlayerRef()) return;
            OnCanRotate?.Invoke(true);
            OnMovementSpeedChanged?.Invoke(_stats.IdleSpeed);
            
            if (!(_lastSeenTime + _stats.LoseDelay < Time.time)) return;
            _lastSeenTime = Time.time;
            _playerDetected = false;
            Debug.Log("player lost");
            OnChangeTarget?.Invoke(_enemy.GetCurrentPoint());
            OnCanRotate?.Invoke(true);
        }
        else if (!(_enemy.CanSeePlayer() || _enemy.IsAlerted()) && Vector3.Distance(transform.position, _enemy.GetCurrentPoint().position) < 0.1f && !_playerDetected)
        {
            _enemy.ChangePoint();
            Debug.Log("PointReached");
        }
    }
    
    

   
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //OnChangeTarget?.Invoke(_target);
        }
    }
}

