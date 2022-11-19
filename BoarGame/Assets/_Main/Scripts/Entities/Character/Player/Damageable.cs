using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : Subject, IDamageable
{
    private CharacterSO _stats;
    private bool _isInvulnerable;
    [SerializeField] private List<Observer> subscribers;
    public int CurrentLife { get; private set; }

    public override List<Observer> Subscribers => subscribers;

    private void Awake()
    {
        _stats = GetComponent<Entity>().GetData() as CharacterSO;
    }

    private void Start()
    {
        InitStats();
        if(CompareTag("Enemy") && LevelManager.Instance)
            Subscribe(LevelManager.Instance);
    }

    private void InitStats()
    {
        CurrentLife = _stats.MaxHealth;
        NotifyAll("INIT_LIFE");
    }

    public bool IsAlive()
    {
        return CurrentLife > 0;
    }
    
    public void TakeDamage(int damage)
    {
        if(_isInvulnerable) return;
        if(!IsAlive()) return;

        CurrentLife -= damage;
        NotifyAll("TAKE_DAMAGE", CurrentLife);
        if (IsAlive()) return;
        CurrentLife = 0;
        Die();
    }

    public void AddHealth(int life)
    {
        if(!IsAlive()) return;
        
        CurrentLife += life;
        if (CurrentLife >= _stats.MaxHealth)
            CurrentLife = _stats.MaxHealth;
        NotifyAll("ADD_LIFE", CurrentLife);
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
