using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : Subject
{
    private CharacterSO _stats;
    private bool _isInvulnerable;
    private int _currentLife;
    [SerializeField] private List<Observer> subscribers;

    public override List<Observer> Subscribers => subscribers;

    private void Awake()
    {
        _stats = GetComponent<Entity>().GetData() as CharacterSO;
    }

    private void Start()
    {
        InitStats();
    }

    private void InitStats()
    {
        _currentLife = _stats.MaxHealth;
    }

    private bool IsAlive()
    {
        return _currentLife > 0;
    }
    
    public void TakeDamage(int damage)
    {
        if(_isInvulnerable) return;
        if(!IsAlive()) return;

        _currentLife -= damage;
        NotifyAll("TAKE_DAMAGE", _currentLife);
        if (IsAlive()) return;
        _currentLife = 0;
        Die();
    }

    public void AddHealth(int life)
    {
        if(!IsAlive()) return;
        
        _currentLife += life;
        if (_currentLife >= _stats.MaxHealth)
            _currentLife = _stats.MaxHealth;
        NotifyAll("ADD_LIFE", _currentLife);
    }

    private void Die()
    {
        NotifyAll("DIE", IsAlive());
        gameObject.SetActive(false);
    }

    public override void Subscribe(Observer observer)
    {
        if (subscribers.Contains(observer)) return;
        subscribers.Add(observer);
    }

    public override void Unsubscribe(Observer observer)
    {
        if (subscribers.Contains(observer)) return;
        subscribers.Remove(observer);
    }

    public override void NotifyAll(string message, params object[] args)
    {
        foreach (var t in subscribers)
            t.OnNotify(message, args);
#if UNITY_EDITOR
        Debug.Log(message);
#endif
    }
}
