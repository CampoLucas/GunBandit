using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveDisplay : MonoBehaviour
{
    private List<Observer> subscribers;
    [SerializeField] private TMP_Text mainObjText;
    [SerializeField] private TMP_Text killAllEnemiesObjText;
    [SerializeField] private TMP_Text stealthObjText;
    private void Start()
    {
        if(!LevelManager.Instance) return;
        LevelManager.Instance.OnSeen += StealthFailed;
        LevelManager.Instance.OnKilledAllCompleted += KillAllEnemiesCompleted;
        LevelManager.Instance.OnFullStealthCompleted += StealthCompleted;
        LevelManager.Instance.OnMainObjectiveCompleted += MainCompleted;
    }

    private void MainCompleted() => mainObjText.text = "<s>" + mainObjText.text + "</s>";
    private void KillAllEnemiesCompleted() => killAllEnemiesObjText.text = "<s>" + killAllEnemiesObjText.text + "</s>";
    private void StealthCompleted() => stealthObjText.text = "<s>" + stealthObjText.text + "</s>";
    private void StealthFailed() => stealthObjText.color = Color.gray;
}
