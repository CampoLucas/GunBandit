using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LatchButton : Button
{
    protected override void Press()
    {
        base.Press();
        NotifyAll("TOGGLED");
    }
    
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        _inputs.OnInteractPerformed += Press;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        _inputs.OnInteractPerformed -= Press;
    }
}
