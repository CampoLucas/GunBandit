using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentaryGameButton : GameButton
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
