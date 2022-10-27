using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyAI : MonoBehaviour
{
    private Enemy _enemy;
    private bool _once;
    private float _lastSeenTime;
    
    [Header("Movement components")]
    [SerializeField] private float idleSpeed = 3;
    [SerializeField] private float followSpeed = 1;
    [SerializeField] private float longDistance = 15;
    [SerializeField] private float shortDistance = 10;
    [SerializeField] private float loseDelay = 15;
    
    public Action<Transform> OnChangeTarget;
    public Action<float> OnMovementSpeedChanged;
    public Action<bool> OnCanRotate;
    public Action OnFire;
    public Action<Vector2> OnRotateTowardsPlayer;
    

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        InitStats();
    }

    private void InitStats()
    {
        transform.position = _enemy.GetSpawnPos().position;
    }

    private void Update()
    {
        if (!(_enemy.CanSeePlayer() || _enemy.IsAlerted()) && Vector3.Distance(transform.position, _enemy.GetCurrentPoint().position) < 0.1f)
        {
            _enemy.ChangePoint();
            Debug.Log("PointReached");
        }

        if (_enemy.IsAlerted() && !_enemy.CanSeePlayer())
        {
            
        }
        else
        {
            //OnChangeTarget?.Invoke(_enemy.GetCurrentPoint());
        }
        
        if(!_enemy.GetPlayerRef()) return;
        if (_enemy.CanSeePlayer())
        {
            
            var pos = transform.position;
            var playerTransform = _enemy.GetPlayerRef().transform;
            var playerPos = playerTransform.position;
            var dirToTarget = (playerPos - pos).normalized;
            var distanceToPlayer = Vector3.Distance(pos, playerPos);
            
            OnChangeTarget?.Invoke(playerTransform);
            
            OnCanRotate?.Invoke(false);
            OnRotateTowardsPlayer?.Invoke(playerPos);
            if (distanceToPlayer < longDistance && distanceToPlayer > shortDistance)
            {
                OnFire?.Invoke();
                OnMovementSpeedChanged?.Invoke(followSpeed);
                
            }
            else if (distanceToPlayer < shortDistance)
            {
                OnFire?.Invoke();
                OnMovementSpeedChanged?.Invoke(0.1f);
            }
        }
        else
        {
            OnCanRotate?.Invoke(true);
            OnMovementSpeedChanged?.Invoke(idleSpeed);

            if (!(_lastSeenTime > Time.time)) return;
            _lastSeenTime = Time.time + loseDelay;
            OnChangeTarget?.Invoke(_enemy.GetCurrentPoint());
            OnMovementSpeedChanged?.Invoke(idleSpeed);
            OnCanRotate?.Invoke(true);
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

