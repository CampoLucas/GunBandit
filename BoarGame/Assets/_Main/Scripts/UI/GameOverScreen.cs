using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text enemiesKilledText;
    [SerializeField] private TMP_Text fullStealthText;
    [SerializeField] private TMP_Text allEnemiesKilledText;
    [SerializeField] private Image background;
    [SerializeField] private Image detail;
    
    public void InitStats(bool objectiveCompleted, int killedEnemies, bool fullStealth, bool allEnemiesKilled)
    {
        SetText(ref titleText, "Mission complete", "Mission failed", objectiveCompleted);
        SetText(ref enemiesKilledText, "Enemies killed:", killedEnemies, objectiveCompleted);
        SetText(ref fullStealthText, "Full stealth:", objectiveCompleted, fullStealth);
        SetText(ref allEnemiesKilledText, "All enemies killed:", objectiveCompleted, allEnemiesKilled);
        background.color = objectiveCompleted ? Color.grey : Color.black;
        detail.color = objectiveCompleted ? Color.green : Color.red;
    }

    private void SetText(ref TMP_Text text, in string completedMessage, in string failedMessage, in bool primaryObjective)
    {
        text.text = primaryObjective ? "Mission complete" : "Mission failed";
        text.color = primaryObjective ? Color.green : Color.red;
    }

    private void SetText(ref TMP_Text text, in string message, in bool primaryObjective, in bool secondaryObjective)
    {
        text.enabled = primaryObjective;
        text.text = secondaryObjective ? "<align=left>" + message + "Full stealth<line-height=0><br><align=right>" + "Yes" +
                                  "<line-height=1em>" : "<s>" + "<align=left>Full stealth<line-height=0><br><align=right>" + "No" +
                                                        "<line-height=1em>" + "</s>";
        text.color = primaryObjective ? Color.green : Color.red;
    }
    private void SetText(ref TMP_Text text, in string message, in int amount, in bool primaryObjective)
    {
        text.text = "<align=left>Killed enemies<line-height=0><br><align=right>" + $"{amount:000}" +
                    "<line-height=1em>";
        text.color = primaryObjective ? Color.green : Color.red;
    }
}
