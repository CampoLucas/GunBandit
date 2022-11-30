using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : Observer
{
    private Animator _anim;
    private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    public override void OnNotify(string message, params object[] args)
    {
        switch (message)
        {
            case "TAKE_DAMAGE":
                _anim.SetTrigger(TakeDamage);
                break;
        }
    }
}
