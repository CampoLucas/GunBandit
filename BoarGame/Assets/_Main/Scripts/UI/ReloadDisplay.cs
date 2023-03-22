using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadDisplay : Observer
{
    private Image _image;

    private void Awake()
    {
        _image = GetComponentInChildren<Image>();
    }

    private void Start()
    {
        _image.color = Color.green;
    }

    public override void OnNotify(string message, params object[] args)
    {
        switch (message)
        {
            case "RELOADING":
                _image.color = Color.red;
                return;
            case "RELOADED":
                _image.color = Color.green;
                return;
            case "OUT_OF_AMMO":
                _image.color = Color.gray;
                return;
        }
    }
}
