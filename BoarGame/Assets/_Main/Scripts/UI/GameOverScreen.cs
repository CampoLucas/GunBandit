using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

public class GameOverScreen : MonoBehaviour
{
    private bool _levelWon;
    private TMP_Text _continueText;
    private TMP_Text _mainMenuText;
    
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text enemiesKilledText;
    [SerializeField] private TMP_Text fullStealthText;
    [SerializeField] private TMP_Text allEnemiesKilledText;
    [SerializeField] private Image background;
    [SerializeField] private Image detail;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        _continueText = continueButton.GetComponentInChildren<TMP_Text>();
        _mainMenuText = mainMenuButton.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        continueButton.onClick.AddListener(Continue);
        mainMenuButton.onClick.AddListener(MainMenu);
        if (SceneManager.GetActiveScene().buildIndex + 1 > SceneManager.sceneCount && _levelWon)
            continueButton.gameObject.SetActive(false);
        
        
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(Continue);
        mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    public void InitStats(bool objectiveCompleted, int killedEnemies, bool fullStealth, bool allEnemiesKilled)
    {
        _levelWon = objectiveCompleted;
        SetText(ref titleText, "Mission complete", "Mission failed", _levelWon);
        SetText(ref enemiesKilledText, "Enemies killed:", killedEnemies, _levelWon);
        SetText(ref fullStealthText, "Full stealth:", _levelWon, fullStealth);
        SetText(ref allEnemiesKilledText, "All enemies killed:", _levelWon, allEnemiesKilled);
        SetText(ref _continueText, _levelWon);
        SetText(ref _mainMenuText, _levelWon);
        background.color = objectiveCompleted ? new Color(0.3f, 0.3f, 0.3f, 0.5f) : new Color(0, 0, 0, 0.8f);
        detail.color = objectiveCompleted ? Color.green : Color.red;
    }

    public void Continue()
    {
        SceneManager.LoadScene(_levelWon
            ? SceneManager.GetActiveScene().buildIndex + 1
            : SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void SetText(ref TMP_Text text, in string completedMessage, in string failedMessage, in bool primaryObjective)
    {
        text.text = primaryObjective ? "Mission complete" : "Mission failed";
        text.color = primaryObjective ? Color.green : Color.red;
    }

    private void SetText(ref TMP_Text text, in string message, in bool primaryObjective, in bool secondaryObjective)
    {
        text.enabled = primaryObjective;
        text.text = secondaryObjective ? "<align=left>" + message + "<line-height=0><br><align=right>" + "Yes" +
                                  "<line-height=1em>" : "<s>" + "<align=left>" + message + "<line-height=0><br><align=right>" + "No" +
                                                        "<line-height=1em>" + "</s>";
        text.color = secondaryObjective ? Color.green : new Color(0.7f, 0.7f, 0.7f);
    }
    private void SetText(ref TMP_Text text, in string message, in int amount, in bool primaryObjective)
    {
        text.text = "<align=left>Killed enemies<line-height=0><br><align=right>" + $"{amount:000}" +
                    "<line-height=1em>";
        text.color = primaryObjective ? Color.green : Color.red;
    }

    private void SetText(ref TMP_Text text, in bool primaryObjective)
    {
        text.color = primaryObjective ? Color.green : Color.red;
    }
}
