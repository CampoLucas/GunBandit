using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : Subject, IDamageable
{
    private CharacterSO _stats;
    private bool _isInvulnerable;
    private AnimationController _anim;
    [SerializeField] private List<Observer> subscribers;
    public int CurrentLife { get; private set; }

    public override List<Observer> Subscribers => subscribers;

    private void Awake()
    {
        _stats = GetComponent<Entity>().GetData() as CharacterSO;
        _anim = GetComponent<AnimationController>();
    }

    private void Start()
    {
        InitStats();
        if (LevelManager.Instance)
            Subscribe(LevelManager.Instance);
        if (_anim)
            Subscribe(_anim);
    }

    private void InitStats()
    {
        CurrentLife = _stats.MaxHealth;
        NotifyAll("INIT_LIFE");
    }

    public bool IsAlive() => CurrentLife > 0;
    
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
        if (CompareTag("Enemy")) NotifyAll("DIE", IsAlive());
        if (CompareTag("Player")) NotifyAll("GAME_OVER");
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
    }
}
