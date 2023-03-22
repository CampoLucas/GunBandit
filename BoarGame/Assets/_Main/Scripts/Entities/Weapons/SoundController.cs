using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Observer
{
    private AudioSource _audio;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip reloadClip;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public override void OnNotify(string message, params object[] args)
    {
        switch (message)
        {
            case "FIRE":
                _audio.clip = fireClip;
                _audio.Play();
                break;
            case "RELOADED":
                _audio.clip = reloadClip;
                _audio.Play();
                break;
        }
    }
}
