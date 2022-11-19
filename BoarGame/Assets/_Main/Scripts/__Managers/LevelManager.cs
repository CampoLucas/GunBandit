using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class LevelManager : Observer
{
    //LevelSO? Difficulty?
    private static LevelManager _instance = null;
    private static readonly object Padlock = new object();
    [SerializeField] private GameOverScreen _gameOverScreen;
    [SerializeField] private GameObject _canvas;
    public int Enemies { get; private set; }
    public int EnemiesKilled { get; private set; }
    public bool ObjectiveReached { get; private set; }
    public bool FullStealth { get; private set; }
    public bool AllEnemiesKilled => EnemiesKilled == Enemies;

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
        EnemiesKilled = 0;
        ObjectiveReached = false;
        FullStealth = true;
    }
    
    private void PlayerSeen()
    {
        if (FullStealth)
            FullStealth = false;
    }

    public override void OnNotify(string message, params object[] args)
    {
        switch (message)
        {
            case "ENEMY FOUND":
                Enemies++;
                break;
            case "DIE":
            {
                EnemiesKilled++;
                if (EnemiesKilled >= Enemies)
                {
                    OnKilledAllCompleted?.Invoke();
                    if(FullStealth) OnFullStealthCompleted?.Invoke();
                }
                break;
            }
            case "PLAYER SEEN":
                PlayerSeen();
                OnSeen?.Invoke();
                break;
            case "MAIN OBJECT COMPLETED":
                ObjectiveReached = true;
                OnMainObjectiveCompleted?.Invoke();
                break;
            case "GAME_OVER":
                GameOver();
                break;
        }
    }

    private void GameOver()
    {
        var gameOver = Instantiate(_gameOverScreen, _canvas.transform);
        gameOver.InitStats(ObjectiveReached, EnemiesKilled, FullStealth, AllEnemiesKilled);
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
