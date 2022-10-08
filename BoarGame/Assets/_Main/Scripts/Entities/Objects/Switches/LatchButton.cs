using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatchButton : Button
{
    public override void Press()
    {
        base.Press();
        NotifyAll("Press");
    }
}
