using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelManager : Observer
{
    //LevelSO? Difficulty?
    private static LevelManager _instance = null;
    private static readonly object Padlock = new object();
    [SerializeField] private int enemies;
    [SerializeField] private int enemiesKilled;
    [SerializeField] private bool objectiveReached;
    [SerializeField] private bool fullStealth;

    public Action OnMainObjectiveCompleted;
    public Action OnSeen;
    public Action OnFullStealthCompleted;
    public Action OnKilledAllCompleted;
    
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
        if (fullStealth)
            fullStealth = false;
    }

    public override void OnNotify(string message, params object[] args)
    {
        switch (message)
        {
            case "ENEMY FOUND":
                enemies++;
                break;
            case "DIE":
            {
                enemiesKilled++;
                if (enemiesKilled >= enemies)
                {
                    OnKilledAllCompleted?.Invoke();
                    if(fullStealth) OnFullStealthCompleted?.Invoke();
                }
                break;
            }
            case "PLAYER SEEN":
                PlayerSeen();
                OnSeen?.Invoke();
                break;
            case "MAIN OBJECT COMPLETED":
                objectiveReached = true;
                OnMainObjectiveCompleted?.Invoke();
                break;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
