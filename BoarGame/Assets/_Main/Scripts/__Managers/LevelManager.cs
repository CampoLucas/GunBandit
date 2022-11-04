using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = System.Object;

public sealed class LevelManager : Observer
{
    //LevelSO? Difficulty?
    private static LevelManager _instance = null;
    private static readonly object Padlock = new object();
    [SerializeField] private int enemies;
    [SerializeField] private int enemiesKilled;
    [SerializeField] private bool objectiveReached;
    [SerializeField] private bool fullStealth;

    private LevelManager()
    {
    }

    public static LevelManager Instance
    {
        get
        {
            lock (Padlock)
            {
                return _instance;
            }
        }
    }
    private void Awake()
    {
        lock (Padlock)
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }
        InitLevel();
    }

    private void InitLevel()
    {
        enemiesKilled = 0;
        objectiveReached = false;
        fullStealth = true;
    }
    
    private void PlayerSeen()
    {
        if (fullStealth == true)
            fullStealth = false;
    }

    public override void OnNotify(string message, params object[] args)
    {
        if (message == "DIE")
            enemiesKilled++;
        if (message == "ENEMY FOUND")
            enemies++;
        if (message == "PLAYER SEEN")
            PlayerSeen();
    }
}
