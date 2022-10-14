using System;
using UnityEngine;

public class Entity : MonoBehaviour, IProduct<StatsSO>
{
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private StatsSO stats;
    public StatsSO GetData() => stats;

    protected virtual void Awake()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        InitStats();
    }

    protected virtual void InitStats()
    {
        _spriteRenderer.sprite = stats.Sprite;
    }
    

}
