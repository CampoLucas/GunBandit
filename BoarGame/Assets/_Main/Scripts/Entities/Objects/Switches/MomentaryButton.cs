using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentaryButton : Button
{
    protected override void Press()
    {
        base.Press();
        NotifyAll("ON");
    }
    
    protected override void Release()
    {
        base.Release();
        NotifyAll("OFF");
    }
}
