using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemyAI : MonoBehaviour
{
    private Enemy _enemy;
    
    public Action<Transform> OnChangeTarget;
    public Action<float> OnSpeedChanged;
    public Action OnFire;
    

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
        }

        if (_enemy.IsAlerted() && !_enemy.CanSeePlayer())
        {
            // var randomTransform = new GameObject("empty")
            // {
            //     transform =
            //     {
            //         position = (Vector3)Random.insideUnitCircle * Random.Range(2, 3) + _enemy.GetPlayerRef().transform.position
            //     }
            // };
            // OnChangeTarget?.Invoke(randomTransform.transform);
        }
        
        if (_enemy.CanSeePlayer())
        {
            OnChangeTarget?.Invoke(_enemy.GetPlayerRef().transform);
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

