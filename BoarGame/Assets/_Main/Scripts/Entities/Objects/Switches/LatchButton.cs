using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatchButton : Button
{
    private bool _toggle;
    protected override void Press()
    {
        base.Press();

        if (!_toggle)
        {
            NotifyAll("ON");
            _toggle = true;
        }
        else
        {
            NotifyAll("OFF");
            _toggle = false;
        }
    }
}
