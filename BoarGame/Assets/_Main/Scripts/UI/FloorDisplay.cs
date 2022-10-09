using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FloorDisplay : Observer
{
    [SerializeField] private TMP_Text text;
    public override void OnNotify(string message, params object[] args)
    {
        text.text = message;
    }
}
