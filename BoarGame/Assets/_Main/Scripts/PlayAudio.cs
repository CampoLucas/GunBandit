using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : Observer
{
    private AudioSource _audio;
    [SerializeField] private AudioClip audioClip;
    [Range(-3, 3)] [SerializeField] private float pitch;
    [SerializeField] private string eventName;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    public override void OnNotify(string message, params object[] args)
    {
        if (message != eventName) return;
        _audio.clip = audioClip;
        _audio.pitch = pitch;
        _audio.Play();
    }
}
