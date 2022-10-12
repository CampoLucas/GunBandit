using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGrenade : MonoBehaviour, IAttack
{
    private bool _hasExploded;
    private bool _activated;
    private float _lastActivatedTime;
    private GrenadeSO _stats;

    private void Awake()
    {
        _stats = GetComponent<Grenade>().GetData() as GrenadeSO;
    }

    private void Update()
    {
        if(!_activated || _hasExploded) return;
        _lastActivatedTime += Time.deltaTime;
        if (!(_lastActivatedTime >= _stats.TimeToExplode)) return;
        Explode();
        //if (!(_lastActivatedTime - Time.time < _stats.TimeToExplode)) return;
    }

    public void Attack()
    {
        _activated = true;
    }

    private void Explode()
    {
        _hasExploded = true;
        var transform1 = transform;
        var explosion = Instantiate(_stats.Particles, transform1.position, transform1.rotation);
        
        var onContact = Physics2D.OverlapCircle(transform.position, _stats.Range, _stats.Mask);
        
        if (onContact == null) return;
        
        var direction = (onContact.transform.position - transform.position).normalized;
        var body = onContact.GetComponent<Rigidbody2D>();
            
        if(body != null)
        {
            body.AddForce(direction * (_stats.Force));
        }
        
        Destroy(this.gameObject);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _stats.Range);
    }
}
