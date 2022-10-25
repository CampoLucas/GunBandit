using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Enemy _enemy;
    private Vector3 _startPos;
    private Transform _target;
    
    [Header("Field of View")] 
    

    public Action<Transform> OnChangeTarget;
    public Action<float> OnSpeedChanged;
    public Action OnFire;
    

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
        
    }

    private void Update()
    {
        if (_target == null) return;
        if (_target.gameObject.CompareTag("Player"))
        {
            if (Vector3.Distance(transform.position, _target.position) < 4f)
            {
                OnSpeedChanged?.Invoke(0);
                OnFire?.Invoke();
            }
            else
            {
                OnSpeedChanged?.Invoke(3);
            }
            
        }
    }

   
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnChangeTarget?.Invoke(_target);
        }
    }
}

