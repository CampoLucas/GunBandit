using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 _startPos;
    [SerializeField] private GameObject target;

    [Header("Field of View")] 
    [SerializeField] private float radius = 10;
    [SerializeField] private float angle = 90;
    [SerializeField] private GameObject playerRef;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstaclelMask;
    [SerializeField] private bool canSeePlayer;

    private void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player");
    }

    private IEnumerator FOVRoutine()
    {
        var waitForSeconds = new WaitForSeconds(0.2f);

        while (true)
        {
            
        }
    }
}

